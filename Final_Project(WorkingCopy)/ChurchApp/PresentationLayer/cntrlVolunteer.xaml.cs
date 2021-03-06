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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for cntrlVolunteer.xaml
    /// </summary>
    public partial class cntrlVolunteer : UserControl
    {
        User _user;
        IActivityManager _activityManager;
        IScheduleManager _scheduleManager;
        IUserManager _userManager;

        // This is the constructor for this form, it is used when the form is loaded the standard way from the left side navigation bar.
        public cntrlVolunteer(User user)
        {
            InitializeComponent();
            _user = user;
            _activityManager = new ActivityManager();
            _scheduleManager = new ScheduleManager();
            _userManager = new UserManager();

            populateVolunteerActivities();
            populateUserVolunteerActivities();
            checkVolunteerStatus();

            
        }

        // This is a private method that check to see if the user is a volunteer or not. If the user is a volunteer
        // it indicates the users active status through the volunteer status text box, and shows the activity sign-up
        // and activity un sign-up buttons.
        private void checkVolunteerStatus()
        {
            btnCancelActivity.Visibility = Visibility.Hidden;
            btnSignupActivity.Visibility = Visibility.Hidden;
            btnCancelVolunteer.Visibility = Visibility.Hidden;
            btnApplyVolunteer.Visibility = Visibility.Visible;
            btnApproveDeny.Visibility = Visibility.Hidden;

            txtVolunteerStatus.Text = "Inactive";


            List<string> userRoles = _userManager.RetrievePersonRoles(_user.PersonID);
            foreach (var g in userRoles)
            {
                if (g.ToString() == "Volunteer")
                {
                    txtVolunteerStatus.Text = "Active";
                    btnCancelActivity.Visibility = Visibility.Visible;
                    btnSignupActivity.Visibility = Visibility.Visible;
                    btnCancelVolunteer.Visibility = Visibility.Visible;
                    btnApplyVolunteer.Visibility = Visibility.Hidden;
                }
                if (g.ToString() == "Manager" || g.ToString() == "Pastor")
                {
                    btnApproveDeny.Visibility = Visibility.Visible;
                }


            }
            List<string> unapprovedUserRoles = _userManager.RetrieveUnnaprovedPersonRoles(_user.PersonID);
            foreach(var r in unapprovedUserRoles)
            {
                if(r.ToString() == "Volunteer")
                {
                    txtVolunteerStatus.Text = "Waiting for Approval";
                    btnApplyVolunteer.Visibility = Visibility.Hidden;
                    btnCancelVolunteer.Visibility = Visibility.Visible;
                }
            }

        }

        // This populates the dgUserVolunteerActivities Data Grid
        // This is where activities that the user is currently signed up to volunteer for are displayed
        private void populateUserVolunteerActivities()
        {
            try
            {
                dgUserVolunteerActivities.ItemsSource = _activityManager.RetrieveActivitiesByScheduleType(_user.PersonID, "Volunteer");
            }
            catch (Exception) { }
        }

        // This populates the dgVolunteerActivities Data Grid
        // This is where activities that are available to volunteer for are displayed
        private void populateVolunteerActivities()
        {
            try
            {
                dgVolunteerActivities.ItemsSource = _activityManager.RetrieveActivitiesByActivitySchedule();
            }
            catch (Exception) { }
        }

        // This formats the information that is diplayed in the dgVolunteerActivities Data Grid
        private void DgVolunteerActivities_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgVolunteerActivities.Columns.RemoveAt(14);
            dgVolunteerActivities.Columns.RemoveAt(12);
            dgVolunteerActivities.Columns.RemoveAt(11);
            dgVolunteerActivities.Columns.RemoveAt(10);
            dgVolunteerActivities.Columns.RemoveAt(9);
            dgVolunteerActivities.Columns.RemoveAt(8);
            dgVolunteerActivities.Columns.RemoveAt(7);
            dgVolunteerActivities.Columns.RemoveAt(6);
            dgVolunteerActivities.Columns.RemoveAt(5);
            dgVolunteerActivities.Columns.RemoveAt(4);
            dgVolunteerActivities.Columns.RemoveAt(3);
            dgVolunteerActivities.Columns.RemoveAt(2);
            dgVolunteerActivities.Columns.RemoveAt(0);

            dgVolunteerActivities.Columns[0].Header = "Activity Name";
            dgVolunteerActivities.Columns[1].Header = "Start Time";
        }

        // This formats the information that is diplayed in the dgUserVolunteerActivities Data Grid
        // This is where activities that the user is currently signed up to volunteer for are displayed
        private void DgUserVolunteerActivities_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgUserVolunteerActivities.Columns.RemoveAt(14);
            dgUserVolunteerActivities.Columns.RemoveAt(12);
            dgUserVolunteerActivities.Columns.RemoveAt(11);
            dgUserVolunteerActivities.Columns.RemoveAt(10);
            dgUserVolunteerActivities.Columns.RemoveAt(9);
            dgUserVolunteerActivities.Columns.RemoveAt(8);
            dgUserVolunteerActivities.Columns.RemoveAt(7);
            dgUserVolunteerActivities.Columns.RemoveAt(6);
            dgUserVolunteerActivities.Columns.RemoveAt(5);
            dgUserVolunteerActivities.Columns.RemoveAt(4);
            dgUserVolunteerActivities.Columns.RemoveAt(3);
            dgUserVolunteerActivities.Columns.RemoveAt(2);
            dgUserVolunteerActivities.Columns.RemoveAt(0);

            dgUserVolunteerActivities.Columns[0].Header = "Activity Name";
            dgUserVolunteerActivities.Columns[1].Header = "Start Time";
        }


        // This event handler is fired when the "Sign-up for Activity" button is pushed.
        // This will add a schedule for the user to volunteer for.
        private void BtnSignupActivity_Click(object sender, RoutedEventArgs e)
        {
            // check to see if the user has a list item selected and gives a prompt if
            // they did not
            if(dgVolunteerActivities.SelectedItem == null)
            {
                MessageBox.Show("You must make a volunteering selection.");
                return;
            }
            else
            {
                
                // Checks to make sure the user is not already signed up for this activity
                List<ActivityVM> activities = _activityManager.RetrieveActivitiesByScheduleType(_user.PersonID, "Volunteer");
                foreach (ActivityVM a in activities)
                {
                    if(a.ActivityID == ((ActivityVM)dgVolunteerActivities.SelectedItem).ActivityID)
                    {
                        MessageBox.Show("You are already signed up to volunteer for this activity");
                        return;
                    }
                }

                // This asks the user if they are sure they want to volunteer, and if they choose
                // will be signed up
                if(MessageBox.Show("Are you sure?", "Sign-up to Volunteer", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ActivityVM activity = (ActivityVM)dgVolunteerActivities.SelectedItem;
                    Schedule schedule = new Schedule()
                    {
                        PersonID = _user.PersonID,
                        ActivityID = activity.ActivityID,
                        Type = "Volunteer",
                        ActivitySchedule = false,
                        Start = activity.Start,
                        End = activity.End
                    };
                    if (_scheduleManager.AddSchedule(schedule))
                    {
                        MessageBox.Show("Volunteer Schedule added");
                        populateUserVolunteerActivities();
                        checkVolunteerStatus();
                    }
                    
                }
            }
        }

        // This event handler is fired when the cancel activity button is pushed. It will allows the user to cancel
        // an activity they have previously signed up for
        private void BtnCancelActivity_Click(object sender, RoutedEventArgs e)
        {
            if(dgUserVolunteerActivities.SelectedItem == null)
            {
                MessageBox.Show("You must make a volunteering selection.");
                return;
            }

            if (MessageBox.Show("Are you sure?", "Cancel Volunteering", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    if (_scheduleManager.DeactivateSchedule(((ActivityVM)dgUserVolunteerActivities.SelectedItem).ScheduleID))
                    {
                        MessageBox.Show("Volunteering Canceled.");
                        populateUserVolunteerActivities();
                    }
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        // This event handler is fired when the apply to be a volunteer button is clicked. It signs the user up to be a volunteer and puts
        // the user in the volunteer waitlist. They will have to be approved later by an administrator or manger to actually become a volunteer.
        private void BtnApplyVolunteer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Sign up as a Volunteer", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    if(_userManager.AddUnapprovedPersonRole(_user.PersonID, "Volunteer"))
                    {
                        MessageBox.Show("You have successfully applied to become a volunteer.");
                        checkVolunteerStatus();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        // This event handler is fired when the user clicks the cancel as volunteer button. It cancels the users status as either an applying volunteer,
        // or a fully apporved volunteer.
        private void BtnCancelVolunteer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel as a Volunteer", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    if(_userManager.DeleteUserRole(_user.PersonID, "Volunteer"))
                    {
                        MessageBox.Show("You have successfully canceled being a volunteer.");
                        checkVolunteerStatus();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void BtnApproveDeny_Click(object sender, RoutedEventArgs e)
        {
            frmVolunteerWaitlist volunteerWaitlist = new frmVolunteerWaitlist(_userManager);
            volunteerWaitlist.ShowDialog();
        }
    }
}
