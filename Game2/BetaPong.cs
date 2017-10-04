using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game2
{
    public class BetaPong : Game
    { 
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Kyogre, Groudon, Vaporeon, Flareon, Squirtle, Charmander, Background, Health, Ball, MasterBallRed, Ball2Red, Ball3Red, MasterBallBlue, Ball2Blue, Ball3Blue, Title1, Title2, RedVictory, BlueVictory; //Declaration of parts that need to be drawn
        /*All product names, logos, and brands are property of their respective owners. All company, product and service names used in this game are for fun purposes only.*/
        int LeftPaddleYPosition = 192;
        int RightPaddleYPosition = 192;
        double ballYPosition = 232;
        double ballXPosition = 392;
        double ballXSpeed = 3;
        double ballYSpeed = 3;
        double ballTotalSpeed = Math.Sqrt(21);
        double XYVerhouding = 1;
        int RightPlayerLives = 3;
        int LeftPlayerLives = 3;
        int TitleStatus = 0;
        int TitleCounter = 0;
        int GrootteSpriteRood = 0;
        int GrootteSpriteBlauw = 0;
        //int BallAngle = 0;
        bool GameStart = false;
        bool GameFinished = false;
        Vector2 Titlevector = new Vector2(0, 0);

        public BetaPong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Background = Content.Load<Texture2D>("Background");
            Ball = Content.Load<Texture2D>("PokeBall");

            MasterBallRed = Content.Load<Texture2D>("Master_Ball");
            Ball2Red = Content.Load<Texture2D>("Standard_Pokeball_2");
            Ball3Red = Content.Load<Texture2D>("Standard_Pokeball_2");
            MasterBallBlue = Content.Load<Texture2D>("Master_Ball");
            Ball2Blue = Content.Load<Texture2D>("Standard_Pokeball_2");
            Ball3Blue = Content.Load<Texture2D>("Standard_Pokeball_2");
            Health = Content.Load<Texture2D>("Health");
            Title1 = Content.Load<Texture2D>("Title1");
            Title2 = Content.Load<Texture2D>("Title2");
            RedVictory = Content.Load<Texture2D>("RedVictory");
            BlueVictory = Content.Load<Texture2D>("BlueVictory");

            Kyogre = Content.Load<Texture2D>("Kyogre");
            Groudon = Content.Load<Texture2D>("Groudon");
            Vaporeon = Content.Load<Texture2D>("Vaporeon");
            Flareon = Content.Load<Texture2D>("Flareon");
            Squirtle = Content.Load<Texture2D>("Squirtle");
            Charmander = Content.Load<Texture2D>("Charmander");
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState currentKBState = Keyboard.GetState();
            Random RNG = new Random();

            if (GameStart == false)
            {
                TitleCounter++;
                TitleStatus = TitleCounter % 21;

                if (currentKBState.IsKeyDown(Keys.Space) == true)
                {
                    GameStart = true;
                }
            }

            if (GameStart == true)
            {
                


                if (currentKBState.IsKeyDown(Keys.W) && LeftPaddleYPosition > 0)   //Reads if w is being pressed and it does not escape the boundaries
                {
                    LeftPaddleYPosition = LeftPaddleYPosition - 5;
                }

                if (currentKBState.IsKeyDown(Keys.S) && LeftPaddleYPosition < 480 - GrootteSpriteRood)   //Reads if s is being pressed and it does not escape the boundaries
                {
                    LeftPaddleYPosition = LeftPaddleYPosition + 5;
                }
                if (currentKBState.IsKeyDown(Keys.Up) && RightPaddleYPosition > 0)   //Reads if up is being pressed and it does not escape the boundaries
                {
                    RightPaddleYPosition = RightPaddleYPosition - 5;
                }
                if (currentKBState.IsKeyDown(Keys.Down) && RightPaddleYPosition < 480 - GrootteSpriteBlauw)   //Reads if down is being pressed and it does not escape the boundaries
                {
                    RightPaddleYPosition = RightPaddleYPosition + 5;
                }

                ballXPosition = ballXPosition + ballXSpeed;
                ballYPosition = ballYPosition + ballYSpeed;
                if (ballYPosition <= 0 && ballYSpeed < 0)
                {
                    ballYSpeed = ballYSpeed * -1;
                }
                if (ballYPosition >= 464 && ballYSpeed > 0)
                {
                    ballYSpeed = ballYSpeed * -1;
                }

                if (ballXPosition >= 688 && ballYPosition > RightPaddleYPosition - 16 && ballYPosition < RightPaddleYPosition + GrootteSpriteBlauw && ballXSpeed > 0) //collision detection
                {
                    ballTotalSpeed = ballTotalSpeed + .25;
                    if (ballYPosition <= RightPaddleYPosition + .5 * GrootteSpriteBlauw)
                    {
                        XYVerhouding = (GrootteSpriteBlauw * .5 - (ballYPosition - RightPaddleYPosition))/(.5 * GrootteSpriteBlauw);
                        ballXSpeed = ballTotalSpeed / (Math.Sqrt(XYVerhouding * XYVerhouding + 1));
                        ballYSpeed = -1 * ballXSpeed * XYVerhouding;
                        ballXSpeed = ballXSpeed * -1;

                    } 
                    if (ballYPosition > RightPaddleYPosition + .5 * GrootteSpriteBlauw)
                    {
                        XYVerhouding = ((ballYPosition - RightPaddleYPosition) - .5 * GrootteSpriteBlauw) / (.5 * GrootteSpriteBlauw);
                        ballXSpeed = ballTotalSpeed / (Math.Sqrt(XYVerhouding * XYVerhouding + 1));
                        ballYSpeed = ballXSpeed * XYVerhouding;
                        ballXSpeed = ballXSpeed * -1;
                    }
                    
                    
                    /* ballXSpeed = ballXSpeed * -1 - 1;
                    ballYSpeed++; */
                }
                if (ballXPosition <= 96 && ballYPosition > LeftPaddleYPosition - 16 && ballYPosition < LeftPaddleYPosition + GrootteSpriteRood && ballXSpeed < 0) //collision detection
                {

                    ballTotalSpeed = ballTotalSpeed + .25;
                    if (ballYPosition <= LeftPaddleYPosition + .5 * GrootteSpriteRood)
                    {
                        XYVerhouding = (GrootteSpriteRood * .5 - (ballYPosition - LeftPaddleYPosition)) / (.5 * GrootteSpriteRood);
                        ballXSpeed = ballTotalSpeed / (Math.Sqrt(XYVerhouding * XYVerhouding + 1));
                        ballYSpeed = -1 * ballXSpeed * XYVerhouding;
                    }
                    if (ballYPosition > LeftPaddleYPosition + .5 * GrootteSpriteRood)
                    {
                        XYVerhouding = ((ballYPosition - LeftPaddleYPosition) - .5 * GrootteSpriteRood) / (.5 * GrootteSpriteRood);
                        ballXSpeed = ballTotalSpeed / (Math.Sqrt(XYVerhouding * XYVerhouding + 1));
                        ballYSpeed = ballXSpeed * XYVerhouding;
                    }
                }
                if (ballXPosition <= 80)
                {
                    LeftPlayerLives--;
                    if (GameFinished == true)
                    {
                        if (currentKBState.IsKeyDown(Keys.R) == true)
                        {
                            TitleStatus = 0;
                            GameFinished = false;
                            GameStart = false;
                            RightPlayerLives = 3;
                            LeftPlayerLives = 3;
                        }
                    }
                    ballYPosition = 232;
                    ballXPosition = 392;



                    
                    double OpstartVerhouding = RNG.NextDouble();
                    ballYSpeed = Convert.ToSingle( ballTotalSpeed * OpstartVerhouding * .5 );

                    double XNeg = RNG.Next(1, 100);

                    if (XNeg <= 50)
                    {
                        XNeg = -1;
                    }
                    else
                    {
                        XNeg = 1;
                    }
                    ballXSpeed = ballTotalSpeed - ballYSpeed;
                    ballXSpeed = ballXSpeed * XNeg;
                    
                    double YNeg = RNG.Next(1, 100);

                    if (YNeg <= 50)
                    {
                        YNeg = -1;
                    }
                    else
                    {
                        YNeg = 1;
                    }

                    ballYSpeed = ballYSpeed * YNeg;
                    ballTotalSpeed = Math.Sqrt(18);


                    LeftPaddleYPosition = 192;
                    RightPaddleYPosition = 192;
                }
                if (ballXPosition >= 704)
                {
                    RightPlayerLives--;
                    if (GameFinished == true)
                    {
                        if (currentKBState.IsKeyDown(Keys.R) == true)
                        {
                            TitleStatus = 0;
                            GameFinished = false;
                            GameStart = false;
                            RightPlayerLives = 3;
                            LeftPlayerLives = 3;
                            
                        }
                    }
                    ballYPosition = 232;
                    ballXPosition = 392;


                    double OpstartVerhouding = RNG.NextDouble();
                    ballYSpeed = Convert.ToSingle(ballTotalSpeed * OpstartVerhouding * .5);

                    double XNeg = RNG.Next(1, 100);

                    if (XNeg <= 50)
                    {
                        XNeg = -1;
                    }
                    else
                    {
                        XNeg = 1;
                    }
                    ballXSpeed = ballTotalSpeed - ballYSpeed;
                    ballXSpeed = ballXSpeed * XNeg;

                    double YNeg = RNG.Next(1, 100);

                    if (YNeg <= 50)
                    {
                        YNeg = -1;
                    }
                    else
                    {
                        YNeg = 1;
                    }

                    ballYSpeed = ballYSpeed * YNeg;
                    ballTotalSpeed = Math.Sqrt(18);


                    LeftPaddleYPosition = 192;
                    RightPaddleYPosition = 192;
                }
            }
            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Vector2 Backy = new Vector2(0, 0);
            Vector2 HealthPosition = new Vector2(0, 10);
            Vector2 VRedHP1 = new Vector2(225, 20);
            Vector2 VRedHP2 = new Vector2(200, 20);
            Vector2 VRedHP3 = new Vector2(175, 20);
            Vector2 VBlueHP1 = new Vector2(559, 20);
            Vector2 VBlueHP2 = new Vector2(584, 20);
            Vector2 VBlueHP3 = new Vector2(609, 20);
            Vector2 RightPaddleVector = new Vector2(704, RightPaddleYPosition);
            Vector2 LeftPaddleVector = new Vector2(0, LeftPaddleYPosition);

            float XPosDouble = System.Convert.ToSingle(ballXPosition);
            float YPosDouble = System.Convert.ToSingle(ballYPosition);

            Vector2 BallVector = new Vector2(XPosDouble, YPosDouble);
            Vector2 SpritePos = new Vector2(400, 10);
            Vector2 Titlevector = new Vector2(0, 0);

            if (GameStart == false && TitleStatus == 10)
            {
                spriteBatch.Draw(Title1, Titlevector);
            }
            if (GameStart == false && TitleStatus == 20)
            {
                spriteBatch.Draw(Title2, Titlevector);
            }
            if (GameStart == true && GameFinished == false)
            {
                spriteBatch.Draw(Background, Backy);
                spriteBatch.Draw(MasterBallRed, VRedHP1);     //Tekent de goede hoeveelheid levens
                if (LeftPlayerLives >= 2)
                {
                    spriteBatch.Draw(Ball2Red, VRedHP2);
                }
                if (LeftPlayerLives == 3)
                {
                    spriteBatch.Draw(Ball3Red, VRedHP3);
                }
                spriteBatch.Draw(MasterBallBlue, VBlueHP1);
                if (RightPlayerLives >= 2)
                {
                    spriteBatch.Draw(Ball2Blue, VBlueHP2);
                }
                if (RightPlayerLives == 3)
                {
                    spriteBatch.Draw(Ball3Blue, VBlueHP3);
                }
                spriteBatch.Draw(Health, HealthPosition);

                if (LeftPlayerLives == 1)
                {
                    spriteBatch.Draw(Groudon, LeftPaddleVector);
                    GrootteSpriteRood = 144;
                }
                if (LeftPlayerLives == 2)
                {
                    spriteBatch.Draw(Flareon, LeftPaddleVector);
                    GrootteSpriteRood = 120;
                }
                if (LeftPlayerLives == 3)
                {
                    spriteBatch.Draw(Charmander, LeftPaddleVector);
                    GrootteSpriteRood = 96;
                }

                //Selecteerd de goede sprites voor de paddles

                if (RightPlayerLives == 1)
                {
                    spriteBatch.Draw(Kyogre, RightPaddleVector);
                    GrootteSpriteBlauw = 144;
                }
                if (RightPlayerLives == 2)
                {
                    spriteBatch.Draw(Vaporeon, RightPaddleVector);
                    GrootteSpriteBlauw = 120;
                }
                if (RightPlayerLives == 3)
                {
                    spriteBatch.Draw(Squirtle, RightPaddleVector);
                    GrootteSpriteBlauw = 96;
                }

                if (RightPlayerLives == 0)  //Game einde
                {
                    spriteBatch.Draw(RedVictory, Backy);
                    GameFinished = true;
                }
                if (LeftPlayerLives == 0)
                {
                    spriteBatch.Draw(BlueVictory, Backy);
                    GameFinished = true;
                }

                spriteBatch.Draw(Ball, BallVector);
            }
            

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
