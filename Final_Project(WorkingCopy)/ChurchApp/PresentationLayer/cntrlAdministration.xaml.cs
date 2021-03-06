﻿using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for cntrlAdministration.xaml
    /// </summary>
    public partial class cntrlAdministration : UserControl
    {
        // This will hold the information for the user that is loaded from the DAO. this is saved as it is when it is loaded
        // so that it can be sent back to the DAO later for update purposes.
        
        private User _user;

        private IUserManager _userManager;
        private bool _addMode = false;
        
        // The basic form constructor. This constructor uses the UserManager object from the main page so as not to 
        // uneccessarily fill the memory with unneeded reference objects.
        public cntrlAdministration(IUserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;

            populateUserDataGrid();
            btnEdit.Visibility = Visibility.Hidden;

        }

        // This function retrieves a list of active users from the DAO and uses that list to populate a data grid.
        private void populateUserDataGrid()
        {
            try
            {
                if (dgUserList.ItemsSource == null)
                {
                    dgUserList.ItemsSource = _userManager.RetrieveUserListByActive();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }
        }

        // When the User List data grid's columns are generated automatically this function formats the rows in
        // a way that is more appealing and easier to read. It's purpose is to format the columns into a more 
        // human readable form as well as remove any columns that are not need in the current context. Further
        // user details will be provided later when an item in the list is double clicked.
        private void DgUserList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgUserList.Columns.Remove(dgUserList.Columns[13]);
            dgUserList.Columns.Remove(dgUserList.Columns[12]);
            dgUserList.Columns.Remove(dgUserList.Columns[11]);
            dgUserList.Columns.Remove(dgUserList.Columns[10]);
            dgUserList.Columns.Remove(dgUserList.Columns[9]);
            dgUserList.Columns.Remove(dgUserList.Columns[8]);
            dgUserList.Columns.Remove(dgUserList.Columns[7]);
            dgUserList.Columns.Remove(dgUserList.Columns[6]);
            dgUserList.Columns.Remove(dgUserList.Columns[4]);
            dgUserList.Columns.Remove(dgUserList.Columns[3]);

            dgUserList.Columns[0].Header = "Person ID";
            dgUserList.Columns[1].Header = "First Name";
            dgUserList.Columns[2].Header = "Last Name";
            dgUserList.Columns[3].Header = "Email";
        }
        
        // This method populates the roles list box with the roles that are not assigned to the user and populates
        // the user roles list box with roles that are currently assigned to the user
        private void populateRoles()
        {
            try
            {
                var userRoles = _userManager.RetrievePersonRoles(_user.PersonID);
                lbUserRoles.ItemsSource = userRoles;

                var roles = _userManager.RetrievePersonRoles();

                foreach (string r in userRoles)
                {
                    roles.Remove(r);
                }
                lbRoles.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        // This method populates the groups list box with the groups that are not assigned to the user and populates
        // the user groups list box with groups that are currently assigned to the user
        private void populateGroups()
        {
            try
            {
                var userGroups = _userManager.RetrievePersonGroups(_user.PersonID);
                lbUserGroups.ItemsSource = userGroups;

                var groups = _userManager.RetrievePersonGroups();

                foreach (string g in userGroups)
                {
                    groups.Remove(g);
                }
                lbGroups.ItemsSource = groups;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        // This event handler is fired when the roles to user roles button is clicked. It adds the selected role to
        // the list of user roles
        private void BtnRolesToUserRoles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbRoles.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You must make a role selection.");
                    return;
                }
                if(_addMode)
                {
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Change Role Assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = !(bool)chkActive.IsChecked;
                    return;
                }


                try
                {
                    try
                    {
                        _userManager.DeleteUserRole(_user.PersonID, (string)lbRoles.SelectedItem);
                    }
                    catch (Exception) { }

                    if(_userManager.AddUserRole(_user.PersonID, (string)lbRoles.SelectedItem))
                    {
                        populateRoles();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("You must make a role selection");
            }
        }

        // This event handler is fired when the the user roles to roles button is clicked. It removes the selected role
        // from the user's roles
        private void BtnUserRolesToRoles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbUserRoles.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You must make a role selection.");
                    return;
                }
                if (_addMode)
                {
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Change Role Assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = !(bool)chkActive.IsChecked;
                    return;
                }


                try
                {
                    if (_userManager.DeleteUserRole(_user.PersonID, (string)lbUserRoles.SelectedItem))
                    {
                        populateRoles();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("You must make a role selection");
            }
        }

        // This event handler is fired when the groups to user groups button is clicked. It adds the selected group to
        // the list of user groups
        private void BtnGroupsToUserGroups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbGroups.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You must make a group selection.");
                    return;
                }
                if (_addMode)
                {
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Change Role Assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = !(bool)chkActive.IsChecked;
                    return;
                }


                try
                {
                    try
                    {
                        _userManager.DeleteUserGroup(_user.PersonID, (string)lbGroups.SelectedItem);
                    }
                    catch (Exception) { }

                    
                    if (_userManager.AddUserGroup(_user.PersonID, (string)lbGroups.SelectedItem))
                    {
                        populateGroups();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("You must make a group selection");
            }
        }

        // This event handler is fired when the user groups to groups button is clicked. It removes the selected group
        // from the user's groups
        private void BtnUserGroupsToGroups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbUserGroups.SelectedItems.Count == 0)
                {
                    MessageBox.Show("You must make a group selection.");
                    return;
                }
                if (_addMode)
                {
                    return;
                }
                if (MessageBox.Show("Are you sure?", "Change Role Assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = !(bool)chkActive.IsChecked;
                    return;
                }


                try
                {
                    if (_userManager.DeleteUserGroup(_user.PersonID, (string)lbUserGroups.SelectedItem))
                    {
                        populateGroups();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("You must make a group selection");
            }
        }

        // This event handler is fired when the save button is clicked. It takes all the information that has been entered
        // into the text boxes and either uses it to edit or create a new user.
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            User userToSave = new User();

            if (txtFirstName.Text == "")
            {
                MessageBox.Show("You must enter a first name.");
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text == "")
            {
                MessageBox.Show("You must enter a last name.");
                txtLastName.Focus();
                return;
            }
            if (txtPhoneNumber.Text.ToString().Length < 10)
            {
                MessageBox.Show("You must enter a valid phone number.");
                txtPhoneNumber.Focus();
                return;
            }
            if (!(txtEmail.Text.ToString().Length > 6
                && txtEmail.Text.ToString().Contains("@")
                && txtEmail.Text.ToString().Contains(".")))
            {
                MessageBox.Show("You must enter a valid email address.");
                txtEmail.Focus();
                return;
            }
            if (txtAddress1.Text == "")
            {
                MessageBox.Show("You must enter an Address");
                txtLastName.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                MessageBox.Show("You must enter a City");
                txtLastName.Focus();
                return;
            }
            if (txtState.Text == "")
            {
                MessageBox.Show("You must enter a State");
                txtLastName.Focus();
                return;
            }
            if (txtDob.Text == "")
            {
                MessageBox.Show("You must enter a Date of Birth");
                txtLastName.Focus();
                return;
            }


            try
            {
                User newUser = new User()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Dob = DateTime.Parse(txtDob.Text),
                    PhoneNumber = txtPhoneNumber.Text,
                    Email = txtEmail.Text,
                    Address1 = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    City = txtCity.Text,
                    State = txtState.Text,
                    Zip = txtZip.Text,
                    
                };

                if (_addMode)
                {
                    _userManager.AddUser(newUser);
                    MessageBox.Show("Created User: " + newUser.FirstName + " " + newUser.LastName);
                    clearText();
                    btnSave.Visibility = Visibility.Hidden;
                    btnNew.Visibility = Visibility.Visible;
                    _addMode = false;
                }
                else
                {
                    if (_userManager.EditUser(_user, newUser))
                    {
                        MessageBox.Show("Updated User: " + newUser.FirstName + " " + newUser.LastName);
                        //disableTextBoxes();
                        //clearText();
                    }
                    else
                    {
                        throw new ApplicationException("Data not Saved.", new ApplicationException("User may no longer exist."));

                    }
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("You must enter a Dob in the MM/DD/YYYY format\n" + fe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            clearText();
            btnSave.Visibility = Visibility.Hidden;
            btnNew.Visibility = Visibility.Visible;
            _addMode = false;
            refreshUserList();
            disableTextBoxes();
            //clearText();
            //lbGroups.Items.Refresh();
            //lbRoles.Items.Refresh();
            //lbUserGroups.Items.Refresh();
            //lbUserRoles.Items.Refresh();

        }

        // This event handler is fired when the create new user button is clicked. It clears and then enable all the text boxes,
        // allowing the user to enter the information needed to add a new user.
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addMode = true;
                enableTextBoxes();
                clearText();
                chkActive.IsChecked = true;
                lbGroups.ItemsSource = null;
                lbUserGroups.ItemsSource = null;
                lbRoles.ItemsSource = null;
                lbUserRoles.ItemsSource = null;
                btnNew.Visibility = Visibility.Hidden;
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
            }
            catch (Exception) { }
            
        }
        
        // This helper method simply clears out all the text boxes for this user control.
        private void clearText()
        {
            lblUserId.Content = null;
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtCity.Clear();
            txtDob.Clear();
            txtEmail.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhoneNumber.Clear();
            txtState.Clear();
            txtZip.Clear();
            
        }

        // This helper method simply enables all the text boxes for this user control.
        private void enableTextBoxes()
        {
            txtAddress1.IsEnabled = true;
            txtAddress2.IsEnabled = true;
            txtCity.IsEnabled = true;
            txtDob.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtFirstName.IsEnabled = true;
            txtLastName.IsEnabled = true;
            txtPhoneNumber.IsEnabled = true;
            txtState.IsEnabled = true;
            txtZip.IsEnabled = true;
            chkActive.IsEnabled = true;
        }

        // This helper method disables all the user controls for this user control.
        private void disableTextBoxes()
        {
            txtAddress1.IsEnabled = false;
            txtAddress2.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtDob.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtFirstName.IsEnabled = false;
            txtLastName.IsEnabled = false;
            txtPhoneNumber.IsEnabled = false;
            txtState.IsEnabled = false;
            txtZip.IsEnabled = false;
            chkActive.IsEnabled = true;
        }

        // This event handler is fired when the when the edit button is clicked. It enables all of the text boxes
        // so that the user can then edit the user information being shown.
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
            enableTextBoxes();
        }

        // This event handler is fired when the check active check box is clicked. It either activates or deactivates
        // the currently displayed user.
        private void ChkActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string caption = (bool)chkActive.IsChecked ? "Reactivate Person" : "Deactivate Person";
                if (MessageBox.Show("Are yopu sure?", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = !(bool)chkActive.IsChecked;
                    return;
                }

                _userManager.SetUserActiveStatus((bool)chkActive.IsChecked, _user.PersonID);
                refreshUserList();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }
        }

        // This method refreshes the list of users shown in the user list data grid.
        private void refreshUserList()
        {
            dgUserList.ItemsSource = _userManager.RetrieveUserListByActive((bool)chkShowActive.IsChecked);
        }

        // This event handler is fired when the show active check box is clicked. It allows the user to toggle between 
        // active and inactive users in the user list data grid
        private void ChkShowActive_Click(object sender, RoutedEventArgs e)
        {
            refreshUserList();
        }

        // This event handler is fired when the dgUserList selection is changed. When this happens the various text boxes
        // are filled with the currently selected item's information and the edit button is made visible.
        private void DgUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _addMode = false;
            try
            {
                disableTextBoxes();
                btnSave.Visibility = Visibility.Hidden;
                btnEdit.Visibility = Visibility.Visible;
                btnNew.Visibility = Visibility.Visible;

                // This is used to store the information of the currently selected user so that that information
                // can be shown on screen.
                _user = (User)dgUserList.SelectedItem;

                // This populates the various fields with their respective property values
                lblUserId.Content = _user.PersonID;
                txtFirstName.Text = _user.FirstName;
                txtLastName.Text = _user.LastName;
                txtPhoneNumber.Text = _user.PhoneNumber;
                txtEmail.Text = _user.Email;
                txtAddress1.Text = _user.Address1;
                txtAddress2.Text = _user.Address2;
                txtCity.Text = _user.City;
                txtState.Text = _user.State;
                txtZip.Text = _user.Zip;
                txtDob.Text = _user.Dob.ToShortDateString();
                chkActive.IsChecked = _user.Active;

                populateRoles();
                populateGroups();
            }
            catch (Exception) { }
        }
    }
}
