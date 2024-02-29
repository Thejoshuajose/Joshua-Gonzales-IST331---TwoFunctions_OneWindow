using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom.Compiler;

namespace Joshua_Gonzales___IST331___TwoFunctions_OneWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// First function: Label Inside Text Box
    /// Second function: Variable Validator for c#
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> bannedWords = new List<string> {"abstract", "do", "in", "protected", "true", "as", "double", "int", "public", "try", "base", "else", "interface",
                                    "readonly", "typeof", "bool", "enum", "internal", "ref", "unit", "break","event","is","return","ulong","byte","explicit",
                                    "lock","sealed","unsafe","catch","false","namespace","short","ushort","checked", "fixed", "null","stackalloc","virtual",
                                    "class","float","object","static","void","const","for","operator","string","volatile","continue","foreach","out","struct",
                                    "while","decimal","goto","override","switch","default","if","params","this","delegate","implicit","private","throw" };
        List<char> bannedSymbols = new List<char> {'!','@','#','$','%','^','&','*','(',')','-','=','+','{','}','[',']','|','"','\'',';',':','<','>',',','?','/','~','`'};
        List<char> bannedNumFirst = new List<char> {'0','1','2','3','4','5','6','7','8','9' };
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void txtFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("In GotFocus() function");
            if (txtFirstName.Text == "First Name")
            {
                txtFirstName.Foreground = new SolidColorBrush(Color.FromRgb(4, 30, 66));
                txtFirstName.Text = "";
            }
        }

        private void txtFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text == "First Name" | txtFirstName.Text == "")
            {
                txtFirstName.Foreground = new SolidColorBrush(Color.FromRgb(225, 108, 108));
                txtFirstName.Text = "First Name";

            }

        }

        private void txtLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLastName.Text == "Last Name" | txtLastName.Text == "")
            {
                txtLastName.Foreground = new SolidColorBrush(Color.FromRgb(225, 108, 108));
                txtLastName.Text = "Last Name";
            }

        }

        private void txtLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLastName.Text == "Last Name")
            {
                txtLastName.Foreground = new SolidColorBrush(Color.FromRgb(4, 30, 66));
                txtLastName.Text = "";
            }

        }

        private void btnShowName_Click(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = txtLastName.Text + ", " + txtFirstName.Text;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = "";
            txtLastName.Foreground = new SolidColorBrush(Color.FromRgb(225, 108, 108));
            txtLastName.Text = "Last Name";
            txtFirstName.Foreground = new SolidColorBrush(Color.FromRgb(225, 108, 108));
            txtFirstName.Text = "First Name";

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you Sure?", "Do you want to Exit?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

        }

        private void btnFunction1_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Visibility = Visibility.Visible;
            txtLastName.Visibility = Visibility.Visible;
            txtFullName.Visibility = Visibility.Visible;
            lblFullName.Visibility = Visibility.Visible;
            btnClear.Visibility = Visibility.Visible;
            btnShowName.Visibility = Visibility.Visible;
            txtbInvalid.Visibility = Visibility.Hidden;
            txtbValid.Visibility = Visibility.Hidden;
            txtVariableName.Visibility = Visibility.Hidden;
            lblWelcomeMessage.Visibility = Visibility.Hidden;

        }

        private void btnFunction2_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Visibility = Visibility.Hidden; 
            txtLastName.Visibility = Visibility.Hidden;
            txtFullName.Visibility = Visibility.Hidden; 
            lblFullName.Visibility = Visibility.Hidden;
            btnClear.Visibility = Visibility.Hidden;
            btnShowName.Visibility = Visibility.Hidden;
            txtVariableName.Visibility = Visibility.Visible;
            lblWelcomeMessage.Visibility = Visibility.Hidden;
            txtbInvalid.Visibility = Visibility.Hidden;
            txtbValid.Visibility = Visibility.Hidden;



        }

        

        private void txtVariableName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtVariableName.Text == "Variable Name")
            {
                txtVariableName.Clear();
                txtbInvalid.Visibility = Visibility.Hidden;
                txtbValid.Visibility = Visibility.Hidden;

            }
        }

        private void txtVariableName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtVariableName.Text == "")
            {
                txtbValid.Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (char v in bannedSymbols)
                    if (txtVariableName.Text.Contains(v))
                    {
                        txtbInvalid.Visibility = Visibility.Visible;
                        txtbValid.Visibility = Visibility.Hidden;
                    }
                foreach (char n in bannedNumFirst)
                    if (txtVariableName.Text.StartsWith(n))
                    {
                        txtbInvalid.Visibility = Visibility.Visible;
                        txtbValid.Visibility = Visibility.Hidden;
                    }
                    else if (bannedWords.Contains(txtVariableName.Text))
                    {
                        txtbInvalid.Visibility = Visibility.Visible;
                        txtbValid.Visibility = Visibility.Hidden;
                        txtVariableName.Foreground = new SolidColorBrush(Color.FromRgb(225, 108, 108));
                        MessageBox.Show("The variable name entered is invalid as it is one of the reseverd words for c#");
                    }
                    else if (new Microsoft.CSharp.CSharpCodeProvider().IsValidIdentifier(txtVariableName.Text))
                    {
                        txtbInvalid.Visibility = Visibility.Hidden;
                        txtbValid.Visibility = Visibility.Visible;
                    }
                    if (txtVariableName.Text == "")
                    {
                        txtbValid.Visibility = Visibility.Hidden;
                    }
            }
          

        }

        private void txtVariableName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtVariableName.Text == "" )
            {
                txtVariableName.Text = "Variable Name";
                txtbInvalid.Visibility = Visibility.Hidden;
                txtbValid.Visibility = Visibility.Hidden;
            }
        }
       
      
    }
}