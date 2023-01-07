using QPacity.Properties;
using QPacity.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace QPacity.ViewModel
{
    class RoomDimensionsPopUpViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A list containing all possible room shapes, loaded from the custom variable type <see cref="RoomShape"/> 
        /// </summary>
        public List<string> RoomShapes { get; set; } = Enum.GetValues(typeof(RoomShape)).Cast<RoomShape>().Select(s => s.ToString()).ToList();

        /// <summary>
        /// A private integer storeing the currently selected room shape's index, with default value as the last selected room shape.
        /// </summary>
        private int _roomShapeIndex { get; set; } = Settings.Default.RoomShapeIndex;

        /// <summary>
        /// The currently selected room shape's index.
        /// </summary>
        public int RoomShapeIndex
        {
            get
            {
                return _roomShapeIndex;
            }
            set
            {
                _roomShapeIndex = value;

                // Update the room's area
                UpdateRoomArea();
                SaveRoomSettings();
            }
        }

        #region Room Dimensions

        /// <summary>
        /// A private variable storing the length of the room, if it is rectangular.
        /// </summary>
        public string _rectangularLength { get; set; } = Settings.Default.RectangularLength;

        /// <summary>
        /// The length of the room, if it is rectangular.
        /// </summary>
        public string RectangularLength
        {
            get
            {
                return _rectangularLength;
            }
            set
            {
                // Clamp the input value to a positive double
                _rectangularLength = ValidationHelpers.ValidatePositiveDouble(value);

                // Update the room's area
                UpdateRoomArea();
                SaveRoomSettings();
            }
        }

        /// <summary>
        /// A private variable storing the width of the room, if it is rectangular.
        /// </summary>
        public string _rectangularWidth { get; set; } = Settings.Default.RectangularWidth;

        /// <summary>
        /// The width of the room, if it is rectangular.
        /// </summary>
        public string RectangularWidth
        {
            get
            {
                return _rectangularWidth;
            }
            set
            {
                // Clamp the input value to a positive double
                _rectangularWidth = ValidationHelpers.ValidatePositiveDouble(value);

                // Update the room's area
                UpdateRoomArea();
                SaveRoomSettings();
            }
        }

        /// <summary>
        /// A private variable storing the radius of the room, if it is circular.
        /// </summary>
        public string _circularRadius { get; set; } = Settings.Default.CircularRadius;

        /// <summary>
        /// The radius of the room, if it is circular.
        /// </summary>
        public string CircularRadius
        {
            get
            {
                return _circularRadius;
            }
            set
            {
                // Clamp the input value to a positive double
                _circularRadius = ValidationHelpers.ValidatePositiveDouble(value);

                // Update the room's area
                UpdateRoomArea();
                SaveRoomSettings();
            }
        }

        /// <summary>
        /// A private variable storing the area of the room, if it is of a custom shape.
        /// </summary>
        public string _customArea { get; set; } = Settings.Default.CustomArea;

        /// <summary>
        /// The radius of the room, if it is of a custom shape.
        /// </summary>
        public string CustomArea
        {
            get
            {
                return _customArea;
            }
            set
            {
                // Clamp the input value to a positive double
                _customArea = ValidationHelpers.ValidatePositiveDouble(value);

                // Update the room's area
                UpdateRoomArea();
                SaveRoomSettings();
            }
        }

        #endregion

        /// <summary>
        /// The room's area.
        /// </summary>
        public double RoomArea { get; set; } = Settings.Default.RoomArea;

        #endregion

        #region Public Commands

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="window"></param>
        public RoomDimensionsPopUpViewModel()
        {
            // Core Pop-up functionality connection
            ClosePopUpCommand = new RelayCommand(() => ClosePopUp());
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Calculates the room's area based on its shape and dimensions.
        /// </summary>
        private void UpdateRoomArea()
        {
            // Initialise a new double variable to store the area.
            double area = new double();

            // Calculate the area based on the room's shape
            switch ((RoomShape)RoomShapeIndex)
            {
                // If the room is rectangular...
                case RoomShape.Rectangular:
                    area = Convert.ToDouble(RectangularLength) * Convert.ToDouble(RectangularWidth);
                    break;

                // If the room is circular...
                case RoomShape.Circular:
                    area = Math.PI * Convert.ToDouble(CircularRadius) * Convert.ToDouble(CircularRadius);
                    break;

                // If the room is of a custom shape...
                case RoomShape.Custom:
                    area = Convert.ToDouble(CustomArea);
                    break;

                // If some kind of error has occured...
                default:
                    area = 0;
                    break;
            }

            // Round the room's area to two decimal places and display it
            RoomArea = Math.Round(area, 2);
        }

        /// <summary>
        /// Saves the rooms dimensions to local storage for reuse across the application.
        /// </summary>
        private void SaveRoomSettings()
        {
            // Write the room's properties to the application's settings
            Settings.Default.RoomShapeIndex = RoomShapeIndex;

            Settings.Default.RectangularLength = RectangularLength;
            Settings.Default.RectangularWidth = RectangularWidth;
            Settings.Default.CircularRadius = CircularRadius;
            Settings.Default.CustomArea = CustomArea;

            Settings.Default.RoomArea = RoomArea;

            // Save the settings
            Settings.Default.Save();
        }

        #endregion

        #region Pop-Up Core

        /// <summary>
        /// The command for when a pop-up is closed
        /// </summary>
        public ICommand ClosePopUpCommand { get; set; }

        /// <summary>
        /// Closes the current pop-up.
        /// </summary>
        private void ClosePopUp()
        {
            PopUpAggregator.BroadcastDeletion();
        }

        #endregion

    }
}
