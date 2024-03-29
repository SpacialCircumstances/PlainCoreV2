﻿using PlainCore.Graphics.Core;
using PlainCore.System;
using System.Numerics;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Describes a high level view controlling the view area on the screen
    /// </summary>
    public class View
    {
        /// <summary>
        /// Creates a new view.
        /// </summary>
        /// <param name="viewport">The initial viewport</param>
        /// <param name="position">The view position</param>
        /// <param name="size">The view size</param>
        /// <param name="rotation">The view rotation. Defaults to 0</param>
        public View(Viewport viewport, Vector2 position, Vector2 size, float rotation = 0f)
        {
            this.viewport = viewport;
            this.position = position;
            this.size = size;
            this.rotation = rotation;
        }

        /// <summary>
        /// Creates a new view from a view rectangle, taking it as viewport and as view definition.
        /// </summary>
        /// <param name="rect">The view rectangle</param>
        /// <param name="rotation">The view rotation. Defaults to 0</param>
        public View(FloatRectangle rect, float rotation = 0f)
        {
            this.viewport = new Viewport((int)rect.Position.X, (int)rect.End.Y, (int)rect.End.X, (int)rect.Position.Y);
            this.position = rect.Position;
            this.size = rect.Size;
            this.rotation = rotation;
        }

        protected Viewport viewport;
        protected Matrix4x4 worldMatrix;
        protected Matrix4x4 inverseWorldMatrix;
        protected bool recomputeWorldMatrix = true;

        /// <summary>
        /// Allows getting and setting the views viewport.
        /// </summary>
        public Viewport Viewport
        {
            get => viewport;
            set
            {
                viewport = value;
            }
        }

        /// <summary>
        /// Allows getting the computed world matrix.
        /// </summary>
        /// <remarks>Lazy call</remarks>
        public Matrix4x4 WorldMatrix => LazyComputeWorldMatrix();

        protected Vector2 position;
        protected Vector2 size;
        protected float rotation;

        /// <summary>
        /// Gets/sets the view position.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                recomputeWorldMatrix = true;
            }
        }

        /// <summary>
        /// Gets/sets the view size.
        /// </summary>
        public Vector2 Size
        {
            get => size;
            set
            {
                size = value;
                recomputeWorldMatrix = true;
            }
        }

        /// <summary>
        /// Gets/sets the view rotation.
        /// </summary>
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                recomputeWorldMatrix = true;
            }
        }

        protected Matrix4x4 LazyComputeWorldMatrix()
        {
            if (recomputeWorldMatrix)
            {
                recomputeWorldMatrix = false;
                var rotationMatrix = Matrix4x4.CreateRotationZ(rotation);
                var projectionMatrix = Matrix4x4.CreateOrthographic(size.X, size.Y, -1f, 1f);
                var translationMatrix = Matrix4x4.CreateTranslation(position.X - (size.X / 2), position.Y - (size.Y / 2), 0f);
                worldMatrix = translationMatrix * projectionMatrix * rotationMatrix;
                var success = Matrix4x4.Invert(worldMatrix, out inverseWorldMatrix);
                if (!success) inverseWorldMatrix = Matrix4x4.Identity; //At least it does not crash anything
            }

            return worldMatrix;
        }

        protected Matrix4x4 LazyComputeInverseWorldMatrix()
        {
            if (recomputeWorldMatrix)
            {
                LazyComputeWorldMatrix();
            }

            return inverseWorldMatrix;
        }

        /// <summary>
        /// Convert world coordinates to screen coordinates.
        /// </summary>
        /// <param name="coords">Coordinates in the world coordinate system</param>
        /// <returns>Coordinates in the screen coordinate system</returns>
        public Vector2 WorldToScreenCoordinates(Vector2 coords)
        {
            var transformed = Vector2.Transform(coords, LazyComputeWorldMatrix());
            float x = (transformed.X + 1f) / 2f * viewport.Width;
            float y = (transformed.Y + 1f) / 2f * viewport.Height;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Convert screen coordinates to world coordinates.
        /// </summary>
        /// <param name="coords">Coordinates in the screen coordinate system</param>
        /// <returns>Coordinates in the world coordinate system</returns>
        public Vector2 ScreenToWorldCoordinates(Vector2 coords)
        {
            float x = -1f + (2f * (coords.X - viewport.Left) / viewport.Width);
            float y = -1f + (2f * (coords.Y - viewport.Bottom) / viewport.Height);
            return Vector2.Transform(new Vector2(x, y), LazyComputeInverseWorldMatrix());
        }
    }
}
