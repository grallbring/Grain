using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Grain;

public class ProgramGame : Game
{

    private GraphicsDeviceManager graphicsDeviceManager = null;
    private SpriteBatch spriteBatch = null;
    private Texture2D pixelTexture = null;

    private float totalTime = 0f;
    private Effect shader = null;

    public ProgramGame() : base()
    {
        this.graphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720,
            GraphicsProfile = GraphicsProfile.HiDef
        };

        this.IsFixedTimeStep = false;
        this.InactiveSleepTime = TimeSpan.Zero;
        this.IsMouseVisible = true;
        this.Content.RootDirectory = Domain.CONTENT_DIRECTORY_NAME;
    }

    protected override void LoadContent()
    {
        this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
        this.pixelTexture = new Texture2D(this.GraphicsDevice, 1, 1);

        this.shader = this.Content.Load<Effect>("Shaders/shader");

        this.shader.Parameters["AspectRatio"]?.SetValue(this.GraphicsDevice.Viewport.AspectRatio);

        this.pixelTexture.SetData([Color.White]);
    }

    protected override void Update(GameTime gameTime)
    {
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

        this.totalTime += deltaTime;

        this.shader.Parameters["AspectRatio"]?.SetValue(this.GraphicsDevice.Viewport.AspectRatio);
        this.shader.Parameters["Time"]?.SetValue(this.totalTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.Black);

        this.spriteBatch.Begin(effect: this.shader);

        this.spriteBatch.Draw(
            this.pixelTexture,
            new Rectangle(this.GraphicsDevice.Viewport.X, this.GraphicsDevice.Viewport.Y, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height),
            Color.White);

        this.spriteBatch.End();
    }

}
