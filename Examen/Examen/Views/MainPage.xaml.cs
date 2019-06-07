using Examen.Utils;
using Examen.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Examen.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Salut Dorel,
            //Content:(Stack, Grid, Scroll),
            //Children:{Entry Pref; Label Pref; BoxView Pref; DatePicker; Switch Pref}

            var vm = BindingContext as MainPageViewModel;

            if (!string.IsNullOrEmpty(TextContent.Text))
            {
                await PostDatabase.Instance.UpdateItemData(TextContent.Text);

                string[] words = TextContent.Text.Split(',');
                string[] content;
                string[] children;
                string[] nephiews;

                if (words[1].Contains("Content:"))
                {
                    content = words[1].Split(':');
                    vm.Content = content[1];
                }
                if (words[2].Contains("Children:{") || words[2].Contains("Children: {") || words[2].Contains("Children : {"))
                {
                    children = words[2].Split('{', '}');

                    vm.Children = children[1].Split(';');
                }
                //Salut, Content:Stack, Children:{Entry Pref;Label Pref}
            }
            else
            {
                await DisplayAlert("", "Please enter text", "OK");
            }

            vm.Items = new ObservableCollection<PostItem>(PostDatabase.Instance.GetItemsAsync().Result);

            Layout layout;
            string text;
            List<Entry> entries = new List<Entry>();
            List<Label> labels = new List<Label>();


            switch (vm.Content)
            {
                case "Stack":

                    //layoutChildren = vm.Children.Split
                    foreach (string child in vm.Children)
                    {
                        if (child.Split(' ')[0].Equals("Entry"))
                        {
                            text = child.Split(' ')[1];

                            if (text.Equals("Pref"))
                            {
                                entries.Add(new Entry
                                {
                                    Text = vm.FavoriteColor
                                });

                                entries.Add(new Entry
                                {
                                    Text = vm.FavortieBand
                                });
                            }
                            else
                            {
                                entries.Add(new Entry
                                {
                                    Text = text
                                });
                            }
                            
                        }


                        //if (child.Split(' ')[0].Equals("Label")) {
                        //    text = child.Split(' ')[1];

                        //    labels.Add(new Label
                        //    {
                        //        Text = 
                        //    }
                        //}
                    }

                    StackLayout sl = new StackLayout();

                    foreach (Entry entry in entries)
                    {
                        sl.Children.Add(entry);
                    }

                    Page page = new ContentPage()
                    {
                        Content = sl

                    };

                    await Navigation.PushAsync(page);

                    break;

                case "Grid":
                    layout = new Grid();
                    break;

                case "Scroll":
                    layout = new ScrollView();
                    break;
            }

            //Page page = new ContentPage();
            //{

            //    Content = layout
            //    {
            //        Children =
            //    {
            //        new Label
            //        {
            //            Text = "Merge"
            //        }
            //    }
            //    }
            //};


        }
    }
}