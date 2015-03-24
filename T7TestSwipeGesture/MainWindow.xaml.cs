using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Leap;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Threading;

namespace T7TestSwipeGesture
{
    /// <summary>
    /// Interaction logic for PageTurnningApp.xaml
    /// </summary>
    public partial class MainWindow : Window, ILeapEventDelegate
    {

        private Controller controller = new Controller();



        private LeapListener listener;
        /// <summary>
        /// Count the quantities of swipe gesture
        /// </summary>
        private int countSwipe = 0;
        /// <summary>
        /// Store the image source
        /// </summary>
        private BitmapImage img;
        private BitmapImage img2;

        public MainWindow()
        {
            InitializeComponent();
            //Create controller object in Main window
            this.controller = new Controller();
            //Create Listener
            this.listener = new LeapListener(this);
            controller.AddListener(listener);



        }


        /**create a  delegate, delegate is a keyword, it means method type*/
        delegate void LeapEventDelegate(string EventName);

        /** This method check the event in listener class
           *The activated event's name can be got through this method*/


        public void LeapEventNotification(string EventName)
        {
            if (this.CheckAccess())
            {
                switch (EventName)
                {
                    case "onInit":

                        break;
                    case "onConnect":

                        this.connectHandler();
                        break;
                    case "onFrame":

                        this.checkGestures(this.controller.Frame());

                        break;
                }
            }
            else
            {
                Dispatcher.Invoke(new LeapEventDelegate(LeapEventNotification
                    ), new object[] { EventName });
            }
        }//end method LeapEventNotification

        public void connectHandler()
        {
            this.controller.SetPolicyFlags(Controller.PolicyFlag.POLICY_IMAGES);

            //enable swipe gesture
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
            this.controller.Config.SetFloat("Gesture.Swipe.MinLength", 100.0f);
            this.controller.Config.SetFloat("InteractionBox.Width", 1300.0f);
            this.controller.Config.SetFloat("InteractionBox.Height", 600.0f);

        }


        public void checkGestures(Leap.Frame frame)
        {
            // Get gestures
            GestureList gestures = frame.Gestures();
           
            foreach (Gesture gesture in gestures)
            {
              if(gesture.Type== Gesture.GestureType.TYPE_SWIPE ){

                       //declare a new swipe gesture
                        SwipeGesture swipe = new SwipeGesture(gesture);

                       //pass the swipe gesture's data into labels
                        SwipeDirection.Content =  "Swipe Direction" + swipe.Direction;
                        SwipePosition.Content = "Swipe start Position: " + swipe.StartPosition
                                               +"Swipe position is: " + swipe.Position;

                        SwipeSpeed.Content = "Swipe Speed is " + swipe.Speed;

                        //Specify the images
                        img = new BitmapImage();
                        img2 = new BitmapImage();

                       //only when you begin to act a swipe gesture and your direction is from left to right  will the gesture be counted
                        if (swipe.State == Gesture.GestureState.STATE_START && swipe.Direction.x > 0 && Math.Abs(swipe.Direction.y) < 5) { 
                            
                            countSwipe++;

                          }
                        
                        //use the data to debug
                        Debug.WriteLine(countSwipe);


                      /**change image according to the number of swipe gesture
                       * Intially, the data is 0, 1.jpg will be shown in the right
                       Then 1.jpg will be shown in left and 2.jpg will be shown in right
                       In similar fashion, the left page will show the image that have already be shown in right, to model a turnning page effect,
                       * as if you have turnned a page from right to left*/
                        switch (countSwipe % 12)
                        {
                            case 0:

                               img.BeginInit();
                                img.UriSource = new Uri("Images/1.jpg",UriKind.Relative);
                                img.EndInit();


                                Rightimg.Source = img;
                              
                                break;
                            case 1:

                                img.BeginInit();
                                img.UriSource = new Uri("Images/2.jpg",UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/1.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;

                                break;
                            case 2:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/3.jpg", UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/2.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 3:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/4.jpg", UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/3.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 4:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/5.jpg", UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/4.jpg", UriKind.Relative);
                                img2.EndInit();


                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 5:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/6.jpg", UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/5.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 6:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/7.jpg", UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/6.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 7:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/8.jpg", UriKind.Relative);
                                img.EndInit();

                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/7.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 8:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/9.jpg", UriKind.Relative);
                                img.EndInit();
                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/8.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 9:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/10.jpg", UriKind.Relative);
                                img.EndInit();
                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/9.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 10:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/11.jpg", UriKind.Relative);
                                img.EndInit();
                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/10.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;
                            case 11:
                                img.BeginInit();
                                img.UriSource = new Uri("Images/12.jpg", UriKind.Relative);
                                img.EndInit();
                                img2.BeginInit();
                                img2.UriSource = new Uri("Images/11.jpg", UriKind.Relative);
                                img2.EndInit();

                                Leftimg.Source = img2;
                                Rightimg.Source = img;
                                break;

                        }//end switch
                    
                }//end if
            }//end for each

        }//end checkGestures method

    } //end mainWindows

    public interface ILeapEventDelegate
    {
        //definded a method that can be reused
        void LeapEventNotification(string EventName);
    }

    //listener class
    public class LeapListener : Listener
    {

        //create a interface 
        ILeapEventDelegate eventDelegate;


        //create a constructor with interface argument
        public LeapListener(ILeapEventDelegate delegateObject)
        {
            //create a object of interface
            this.eventDelegate = delegateObject;

        }

        public override void OnInit(Controller controller)
        {
            /**call the LeapEventNotification method in the eventDelegate interface. 
           If the event is activated, the event name can be reported to LeapEventNotification 
             */
            this.eventDelegate.LeapEventNotification("onInit");
        }
        public override void OnConnect(Controller controller)
        {

            this.eventDelegate.LeapEventNotification("onConnect");
        }

        public override void OnFrame(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onFrame");
        }
        public override void OnExit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onExit");
        }
        public override void OnDisconnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onDisconnect");
        }

    }//end of listener class
}
