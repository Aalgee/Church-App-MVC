﻿using System;
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
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for frmFacilitySchedule.xaml
    /// </summary>
    public partial class frmFacilitySchedule : Window
    {
        // This class is used to show the user the facility schedule for all the facilities. It also double as a page where users
        // can view the facilites that they have reserved.

        IBookingManager _bookingManager;
        bool _isReserve;
        User _user;
        IUserManager _userManager;

        // This is the constructor for this class.
        public frmFacilitySchedule(bool isReserve, User user)
        {
            InitializeComponent();
            _isReserve = isReserve;
            _user = user;
            _userManager = new UserManager();

            // if this is a user's reservation schedule
            if (_isReserve)
            {
                this.Title = "Your Reservations";
                lblMyReservations.Content = "My Reservations";
                btnCheckIn.Visibility = Visibility.Hidden;
                btnCheckOut.Visibility = Visibility.Hidden;
            }
            // if this is the all facilities schedule
            else
            {
                this.Title = "Facility Schedule";
            }

            // shows check in and check out buttons
            if (!checkRoles())
            {
                btnCheckIn.Visibility = Visibility.Hidden;
                btnCheckOut.Visibility = Visibility.Hidden;
            }

            _bookingManager = new BookingManager();
            PopulateBookings();
        }

        // This checks to see if the user's roles are either Manager or Pastor and returns true if they are, false if not.
        private bool checkRoles()
        {
            bool result = false;
            List<string> roles = _userManager.RetrievePersonRoles(_user.PersonID);
            foreach(var r in roles)
            {
                if(r == "Manager" || r == "Pastor")
                {
                    result = true;
                }
            }

            return result;
        }

        // This populates the dgBookings data grid.
        public void PopulateBookings()
        {
            try
            {
                // if this is a user's reservation schedule
                if (_isReserve)
                {
                    dgFacilitySchedule.ItemsSource = _bookingManager.RetrieveBookingsByPersonID(_user.PersonID);
                }
                // if this is the all facilities schedule
                else
                {
                    dgFacilitySchedule.ItemsSource = _bookingManager.RetrieveBookingsByActive();
                }
            }
            catch (Exception)
            {

                
            }
            
        }

        // This event handler is fired when the columns for the dgFacilitySchedule data grid are automagically generated. This formats the
        // data grid in such a way that it will be more human readable.
        private void DgFacilitySchedule_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgFacilitySchedule.Columns.RemoveAt(15);
            dgFacilitySchedule.Columns.RemoveAt(14);
            dgFacilitySchedule.Columns.RemoveAt(13);
            dgFacilitySchedule.Columns.RemoveAt(10);
            dgFacilitySchedule.Columns.RemoveAt(9);
            dgFacilitySchedule.Columns.RemoveAt(8);
            dgFacilitySchedule.Columns.RemoveAt(7);
            dgFacilitySchedule.Columns.RemoveAt(6);
            dgFacilitySchedule.Columns.RemoveAt(5);
            dgFacilitySchedule.Columns.RemoveAt(4);
            dgFacilitySchedule.Columns.RemoveAt(2);
            dgFacilitySchedule.Columns.RemoveAt(1);

            dgFacilitySchedule.Columns[0].Header = "Facility Name";
            dgFacilitySchedule.Columns[1].Header = "Facilty Type";
            dgFacilitySchedule.Columns[2].Header = "Scheduled Check Out";
            dgFacilitySchedule.Columns[3].Header = "Scheduled Check In";
        }

        // This event handler fires when the dgFacilitySchedule Selected item is changed. It fills in the user control text boxes with information from the selected
        // item.
        private void DgFacilitySchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(null != dgFacilitySchedule.SelectedItem)
            {
                BookingVM booking = (BookingVM)dgFacilitySchedule.SelectedItem;

                // if the DateTime value is the minimum nothing will be displayed
                if (booking.CheckIn == DateTime.MinValue)
                {
                    txtCheckIn.Text = "";
                }
                else
                {
                    txtCheckIn.Text = booking.CheckIn.ToShortDateString() + " " + booking.CheckIn.ToShortTimeString();
                }

                // if the DateTime value is the minimum nothing will be displayed
                if (booking.CheckOut == DateTime.MinValue)
                {
                    txtCheckOut.Text = "";
                }
                else
                {
                    txtCheckOut.Text = booking.CheckOut.ToShortDateString() + " " + booking.CheckOut.ToShortTimeString();
                }

                txtEmail.Text = booking.PersonEmail;
                txtFacilityName.Text = booking.FacilityName;
                txtFacilityType.Text = booking.FacilityType;
                txtFirstName.Text = booking.PersonFirstName;
                txtLastName.Text = booking.PersontLastName;
                txtPhoneNumber.Text = booking.PersonPhoneNumber;
                txtPricePerHour.Text = booking.PricePerHour.ToString();
                txtScheduledCheckIn.Text = booking.ScheduledCheckIn.ToShortDateString() + " " + booking.ScheduledCheckIn.ToShortTimeString();
                txtScheduledCheckOut.Text = booking.ScheduledCheckOut.ToShortDateString() + " " + booking.ScheduledCheckOut.ToShortTimeString();
                
            }
        }

        // This method clears out all the text boxes in this user control
        public void ClearTextBoxes()
        {
            txtCheckIn.Clear();
            txtCheckOut.Clear();
            txtEmail.Clear();
            txtFacilityName.Clear();
            txtFacilityType.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhoneNumber.Clear();
            txtPricePerHour.Clear();
            txtScheduledCheckIn.Clear();
            txtScheduledCheckOut.Clear();
        }

        // This event handler is fired when the check out button is clicked. It loads the check out form.
        private void BtnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (null != dgFacilitySchedule.SelectedItem)
            {
                frmCheckOut checkOut = new frmCheckOut(true, (Booking)dgFacilitySchedule.SelectedItem, this);
                checkOut.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must make a facility Schedule selection");
            }
        }

        // This event handler is fired when the check in button is clicked. It loads the check out form.
        private void BtnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (null != dgFacilitySchedule.SelectedItem)
            {
                frmCheckOut checkOut = new frmCheckOut(false, (Booking)dgFacilitySchedule.SelectedItem, this);
                checkOut.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must make a facility Schedule selection");
            }
        }
    }
}
