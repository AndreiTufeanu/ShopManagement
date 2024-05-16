using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManagement.Models.BusinessLogicLayer
{
    public class UserBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<User> UsersList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;
        public event EventHandler<bool> OnLogInSuccessfull;
        public static GetAllUsers_Result loggedInUser;

        public UserBL()
        {
            UsersList = new ObservableCollection<User>();
        }

        public void AddMethod(object obj)
        {
            User user = obj as User;
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.username))
                {
                    OperationCompleted?.Invoke(this, "Username must be specified");
                }
                else if (string.IsNullOrEmpty(user.password))
                {
                    OperationCompleted?.Invoke(this, "Password must be specified");
                }
                else
                {
                    try
                    {
                        context.User.Add(user);
                        context.SaveChanges();
                        user.id = context.User.Max(item => item.id);
                        UsersList.Add(user);
                        OperationCompleted?.Invoke(this, $"User {user.username} has been added successfully!");
                    }
                    catch (DbUpdateException)
                    {
                        OperationCompleted?.Invoke(this, "An error occurred while adding the user.");
                        context.User.Remove(user);
                    }
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            User user = obj as User;
            if (user == null)
            {
                OperationCompleted?.Invoke(this, "No user selected!");
                return;
            }
            else if (string.IsNullOrEmpty(user.username))
            {
                OperationCompleted?.Invoke(this, "You must specify a username!");
                return;
            }
            else if (string.IsNullOrEmpty(user.password))
            {
                OperationCompleted?.Invoke(this, "You must specify a password!");
                return;
            }
            try
            {
                context.ModifyUserCredentials(user.id, user.username, user.password);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, $"The user {user.username} was saved successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid user data!");
            }
        }

        public void CheckExistingUser(object obj)
        {
            User user = obj as User;
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.username))
                {
                    OperationCompleted?.Invoke(this, "Username must be specified");
                }
                else if (string.IsNullOrEmpty(user.password))
                {
                    OperationCompleted?.Invoke(this, "Password must be specified");
                }
                else
                {
                    ObjectResult<GetAllUsers_Result> users = context.GetAllUsers();
                    foreach (var individualUser in users)
                    {
                        if (individualUser.username == user.username && individualUser.password == user.password && individualUser.is_active == true)
                        {
                            loggedInUser = individualUser;
                            OnLogInSuccessfull?.Invoke(this, individualUser.user_type == "admin");
                        }
                    }
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            User user = obj as User;
            if (user == null)
            {
                OperationCompleted?.Invoke(this, "You must select a user!");
            }
            else
            {
                context.DeactivateUser(user.id);
                context.SaveChanges();
                UsersList.Remove(user);
                OperationCompleted?.Invoke(this, $"User {user.username} removed successfully!");
            }
        }

        public ObservableCollection<User> GetAllUsers()
        {
            return new ObservableCollection<User>(context.User.ToList());
        }
    }
}
