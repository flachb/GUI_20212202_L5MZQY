using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VectorWars.Core;
using VectorWars.Core.Common;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Effects;
using VectorWars.Core.Elements.Enemies;
using VectorWars.Core.Elements.Projectiles;
using VectorWars.Core.Elements.Turrets;
using Vector = VectorWars.Core.Common.Vector;
using Point = VectorWars.Core.Common.Point;
using System.Windows.Input;
using VectorWars.Core.Elements.Types;

namespace VectorWars
{
    public class Display : FrameworkElement
    {
        private Game _game;
        private readonly Dictionary<Type, ImageSource> _elementBrushes = new Dictionary<Type, ImageSource>();

        private readonly ImageSource _backyardGreenBrush;
        private readonly ImageSource _backyardBlueYellowBrush;
        private readonly ImageSource _backyardStartBrush;
        private readonly ImageSource _backyardEndBrush;

        private IEnumerable<IMapElement> _elementsToDraw;

        public Point MousePositioin { get; set; }

        public Display()
        {
            _backyardBlueYellowBrush = CreateImageBrush("backyard_blueyellow.png");
            _backyardGreenBrush = CreateImageBrush("backyard_green.png");
            _backyardStartBrush = CreateImageBrush("backyard_blueyellow.png");
            _backyardEndBrush = CreateImageBrush("route.png");
        }

        public void SetupModel(Game game)
        {
            _game = game;
            _game.Render += elements =>
            {
                _elementsToDraw = elements;
                Dispatcher.Invoke(InvalidateVisual);
            };
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (_game is null || _elementsToDraw is null)
                return;

            DrawBackground(drawingContext);
            DrawSelectedGrid(drawingContext);
            DrawElements(drawingContext);
        }

        private void DrawBackground(DrawingContext drawingContext)
        {
            foreach (var element in _game.Map.Grid)
            {
                switch (element.Type)
                {
                    case GridElementType.Grass:
                        drawingContext.DrawImage(
                            _backyardGreenBrush,
                            new Rect(
                                (element.Center.X - _game.Map.Grid.SizeOfGrid / 2),
                                (element.Center.Y - _game.Map.Grid.SizeOfGrid / 2),
                                _game.Map.Grid.SizeOfGrid,
                                _game.Map.Grid.SizeOfGrid));
                        break;
                    case GridElementType.Road:
                        drawingContext.DrawImage(
                            _backyardBlueYellowBrush,
                            new Rect(
                                (element.Center.X - _game.Map.Grid.SizeOfGrid / 2),
                                (element.Center.Y - _game.Map.Grid.SizeOfGrid / 2),
                                _game.Map.Grid.SizeOfGrid,
                                _game.Map.Grid.SizeOfGrid));
                        break;
                    case GridElementType.Start:
                        drawingContext.DrawImage(
                            _backyardStartBrush,
                            new Rect(
                                (element.Center.X - _game.Map.Grid.SizeOfGrid / 2),
                                (element.Center.Y - _game.Map.Grid.SizeOfGrid / 2),
                                _game.Map.Grid.SizeOfGrid,
                                _game.Map.Grid.SizeOfGrid));
                        break;
                    case GridElementType.End:
                        drawingContext.DrawImage(
                            _backyardEndBrush,
                            new Rect(
                                (element.Center.X - _game.Map.Grid.SizeOfGrid / 2),
                                (element.Center.Y - _game.Map.Grid.SizeOfGrid / 2),
                                _game.Map.Grid.SizeOfGrid,
                                _game.Map.Grid.SizeOfGrid));
                        break;
                }
            }
        }

        private void DrawSelectedGrid(DrawingContext drawingContext)
        {
            if (MousePositioin == default(Point))
                return;

            var closestGridElement = 
                _game.Map.Grid.OrderBy(
                    g => Point.Distance(
                        g.Center,
                        new Point(
                            (float)MousePositioin.X,
                            (float)MousePositioin.Y)))
                .First();

            if (closestGridElement.OccupiedBy is ITurret turret)
            {
                var rangeBrush = new SolidColorBrush(Colors.Ivory);
                var rangePen = new Pen(Brushes.Black, 5);
                rangeBrush.Opacity = 0.5;
                drawingContext.DrawEllipse(
                    rangeBrush,
                    rangePen,
                    new System.Windows.Point(
                        turret.Position.X,
                        turret.Position.Y),
                    turret.Range,
                    turret.Range);
            }

            if (closestGridElement.Type != GridElementType.Grass
                || closestGridElement.OccupiedBy is not null)
            {
                Mouse.SetCursor(Cursors.Arrow);
                var invalidBrush = new SolidColorBrush(Colors.Red);
                invalidBrush.Opacity = 0.5;
                drawingContext.DrawRectangle(
                    invalidBrush,
                    null,
                    new Rect(
                        (closestGridElement.Center.X - _game.Map.Grid.SizeOfGrid / 2),
                        (closestGridElement.Center.Y - _game.Map.Grid.SizeOfGrid / 2),
                        _game.Map.Grid.SizeOfGrid,
                        _game.Map.Grid.SizeOfGrid));

                return;
            }

            var validBrush = new SolidColorBrush(Colors.LightGreen);
            validBrush.Opacity = 0.5;
            drawingContext.DrawRectangle(
                validBrush,
                null,
                new Rect(
                    (closestGridElement.Center.X - _game.Map.Grid.SizeOfGrid / 2),
                    (closestGridElement.Center.Y - _game.Map.Grid.SizeOfGrid / 2),
                    _game.Map.Grid.SizeOfGrid,
                    _game.Map.Grid.SizeOfGrid));

            Mouse.SetCursor(Cursors.Hand);
        }

        private void DrawElements(DrawingContext drawingContext)
        {
            foreach (var element in _elementsToDraw)
            {
                if (!_elementBrushes.TryGetValue(element.GetType(), out var brush))
                {
                    brush = GetBrushForElement(element);
                    _elementBrushes.Add(element.GetType(), brush);
                }

                drawingContext.PushTransform(
                    new RotateTransform(
                        Vector.AngleBetween(Vector.Up, element.Rotation),
                        element.Position.X,
                        element.Position.Y));

                drawingContext.DrawImage(
                    brush,
                    new Rect(
                        element.Position.X - element.Radius / 2,
                        element.Position.Y - element.Radius / 2,
                        element.Radius,
                        element.Radius));

                drawingContext.Pop();
            }
        }

        private ImageSource GetBrushForElement(IMapElement element)
        {
            return element switch
            {
                MachineGunTurret _ => CreateImageBrush("tower_1star.png"),
                FreezerGunTurret _ => CreateImageBrush("tower_4heart.png"),
                LaserGunTurret _ => CreateImageBrush("tower_2cat.png"),
                RocketLauncherTurret _ => CreateImageBrush("tower_3naruto.png"),

                BulletProjectile _ => CreateImageBrush("shoot_4brown.png"),
                FreezerProjectile _ => CreateImageBrush("shoot_2blue.png"),
                LaserProjectile _ => CreateImageBrush("shoot_3purple.png"),
                RocketProjectile _ => CreateImageBrush("shoot_1red.png"),

                BulletEffect _ => CreateImageBrush("shoot_4brown.png"),
                FreezerEffect _ => CreateImageBrush("shoot_2blue.png"),
                LaserEffect _ => CreateImageBrush("shoot_3purple.png"),
                RocketEffect _ => CreateImageBrush("shoot_1red.png"),

                EmojiEnemy _ => CreateImageBrush("enemy_2angryemoji.png"),
                ManEnemy _ => CreateImageBrush("enemy_5bluewind.png"),
                RabbitEnemy _ => CreateImageBrush("enemy_3redrabbit.png"),
                SkullEnemy _ => CreateImageBrush("enemy_1skull.png"),

                _ => throw new ArgumentOutOfRangeException($"Cannot find element type: {element.GetType()}")
            };
        }
        private ImageSource CreateImageBrush(string imageFileName)
        {
            return new BitmapImage(
                    new Uri(System.IO.Path.Combine("pack://application:,,,/Images", imageFileName),
                    UriKind.RelativeOrAbsolute));
        }

        //private Brush CreateImageBrush(string imageFileName)
        //{
        //    return new ImageBrush(
        //        new BitmapImage(
        //            new Uri(Path.Combine("Images", imageFileName),
        //            UriKind.RelativeOrAbsolute)));
        //}
    }
}
