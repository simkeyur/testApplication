using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Surface;
using Microsoft.Surface.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace SurfaceApplication2
{
    /// <summary>
    /// This is the main type for your application.
    /// </summary>
    public class App1 : Microsoft.Xna.Framework.Game
    {
       GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D light;

       
       

        float elapse;
        bool flag = false;
       float delay =500f;
        int frame = 0;
        string binaryResult;
        Rectangle destRect;
        Rectangle sourceRect;
        int binaryResultLength = 0;
        int binaryposition = 0;

        private TouchTarget touchTarget;
        private Color backgroundColor = new Color(0, 0, 0);
        private bool applicationLoadCompleteSignalled;

        private UserOrientation currentOrientation = UserOrientation.Bottom;
        private Matrix screenTransform = Matrix.Identity;

        /// <summary>
        /// The target receiving all surface input for the application.
        /// </summary>
        protected TouchTarget TouchTarget
        {
            get { return touchTarget; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public App1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

      

        #region Overridden Game Methods

        /// <summary>
        /// Allows the app to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {


            string myText = "go";
            //byte[] arr = System.Text.Encoding.ASCII.GetBytes(myText);
             binaryResult = ConvertToBinary(myText);

             binaryResultLength = binaryResult.Length;
            Console.WriteLine(binaryResult.ToString());
            Console.WriteLine(binaryResultLength.ToString());


          
                

           base.Initialize();
          
        }

        /// <summary>
        /// LoadContent will be called once per app and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            light = Content.Load<Texture2D>("flash");

            // TODO: use this.Content to load your application content here
        }

        /// <summary>
        /// UnloadContent will be called once per app and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the app to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
          
                
                    // TODO: Process touches, 
                    // use the following code to get the state of all current touch points.
                    // ReadOnlyTouchPointCollection touches = touchTarget.GetState();
            //  Add you logis for 1 and 0 to here





            

         
                    elapse += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                  
                    if (elapse >= delay) {
                        Console.WriteLine("elapse time " + elapse.ToString());
                        flag = true;


                        if (binaryposition == binaryResultLength) {

                           flag = false;
                        }

                        if (frame >= 1)
                        {

                           frame = 0;
                          
                         
                        }
                        else {

                            frame++;
                           
                        }

                        elapse = 0;
                      
                    }
                    sourceRect = new Rectangle(0, 0, 100, 100);
                       
                   // destRect = new Rectangle(frame*100, frame*100, 100, 100);
            
            
                   
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the app should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!applicationLoadCompleteSignalled)
            {
                // Dismiss the loading screen now that we are starting to draw
                ApplicationServices.SignalApplicationLoadComplete();
                applicationLoadCompleteSignalled = true;
            }

            //TODO: Rotate the UI based on the value of screenTransform here if desired

            GraphicsDevice.Clear(backgroundColor);
            
            if (flag == true)
            { 
            spriteBatch.Begin();



           


            if (binaryResult[binaryposition] == '0' ) {


                Console.WriteLine(binaryResult[binaryposition].ToString());
            spriteBatch.Draw(light, new Rectangle(100, 100, 100, 100), sourceRect, Color.White);
            
            
            if (binaryposition <= binaryResultLength-1)
            {
                binaryposition++;
            }
                
            }
            else if (binaryResult[binaryposition] == '1')
            {
                Console.WriteLine(binaryResult[binaryposition].ToString());
                spriteBatch.Draw(light, new Rectangle(250, 100, 100, 100), sourceRect, Color.White);
               
                if (binaryposition <=binaryResultLength-1) {
                    binaryposition++;
                }
                
            }
            spriteBatch.End();
            flag = false;
            Console.WriteLine("binaryposition   " + binaryposition);
            
            }

            //TODO: Add your drawing code here
            //TODO: Avoid any expensive logic if application is neither active nor previewed

            base.Draw(gameTime);
        }


        public static string ConvertToBinary(string asciiString)
        {
            string result = string.Empty;
            foreach (char ch in asciiString)
            {
                result += Convert.ToString((int)ch, 2);
            }

            return result;
        }

        #endregion

        #region Application Event Handlers

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: Enable audio, animations here

            //TODO: Optionally enable raw image here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: Optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: Disable audio, animations here

            //TODO: Disable raw image if it's enabled
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Release managed resources.
                IDisposable graphicsDispose = graphics as IDisposable;
                if (graphicsDispose != null)
                {
                    graphicsDispose.Dispose();
                }
                if (touchTarget != null)
                {
                    touchTarget.Dispose();
                    touchTarget = null;
                }
            }

            // Release unmanaged Resources.

            // Set large objects to null to facilitate garbage collection.

            base.Dispose(disposing);
        }

        #endregion
    }
}
