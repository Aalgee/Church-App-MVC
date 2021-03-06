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
    /// Interaction logic for frmVolunteerWaitlist.xaml
    /// </summary>
    public partial class frmVolunteerWaitlist : Window
    {
        // This class is used as a waitlist for users who are waiting to be approved for group membership

        IUserManager _userManager;
        public frmVolunteerWaitlist(IUserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;

            dgVolunteerWaitlist.ItemsSource = _userManager.RetrieveUnapprovedUsersByRoleID("Volunteer");
        }

        // This event handler is fired when the back button is clicked. It closes this form.
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // This event handler is fired when the approve button is clicked. It takes a user that was selected from the waitlist and moves them from the waitlist
        // into full membership
        private void BtnApprove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_userManager.EditPersonRoleAsApproved(((User)dgVolunteerWaitlist.SelectedItem).PersonID, "Volunteer"))
                {
                    MessageBox.Show(((User)dgVolunteerWaitlist.SelectedItem).FirstName + " " + ((User)dgVolunteerWaitlist.SelectedItem).LastName + " added as a Volunteer.");
                    dgVolunteerWaitlist.ItemsSource = _userManager.RetrieveUnapprovedUsersByRoleID("Volunteer");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("You must select a user first");
            }
        }

        // This event handler is fired when the deny button is clicked. This will take a person from the waitlist and remove them, but not add them to the group.
        private void BtnDeny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_userManager.DeleteUserRole(((User)dgVolunteerWaitlist.SelectedItem).PersonID, "Volunteer"))
                {
                    MessageBox.Show(((User)dgVolunteerWaitlist.SelectedItem).FirstName + " " + ((User)dgVolunteerWaitlist.SelectedItem).LastName + " denied as a Volunteer.");
                    dgVolunteerWaitlist.ItemsSource = _userManager.RetrieveUnapprovedUsersByRoleID("Volunteer");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("You must select a user first");
            }
        }

        // This event handler is fired when the columns for the dgAllActivities data grid are automagically generated. This formats the
        // data grid in such a way that it will be more human readable.
        private void DgVolunteerWaitlist_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgVolunteerWaitlist.Columns.RemoveAt(13);
            dgVolunteerWaitlist.Columns.RemoveAt(12);
            dgVolunteerWaitlist.Columns.RemoveAt(11);
            dgVolunteerWaitlist.Columns.RemoveAt(10);
            dgVolunteerWaitlist.Columns.RemoveAt(9);
            dgVolunteerWaitlist.Columns.RemoveAt(8);
            dgVolunteerWaitlist.Columns.RemoveAt(7);
            dgVolunteerWaitlist.Columns.RemoveAt(6);
            dgVolunteerWaitlist.Columns.RemoveAt(5);
            dgVolunteerWaitlist.Columns.RemoveAt(4);
            dgVolunteerWaitlist.Columns.RemoveAt(3);
            dgVolunteerWaitlist.Columns.RemoveAt(0);

            dgVolunteerWaitlist.Columns[0].Header = "First Name";
            dgVolunteerWaitlist.Columns[1].Header = "Last Name";
        }
    }
}
