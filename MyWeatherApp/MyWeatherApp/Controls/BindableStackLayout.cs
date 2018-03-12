using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Xamarin.Forms;

namespace MyWeatherApp.Controls
{


    //stack layout exposing a bindable property to bind dynamically its children from viewmodel
    public class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items), typeof(ObservableCollection<SingleForecastControl>), typeof(BindableStackLayout), null,
                propertyChanged: (b, o, n) =>
                {
                    (n as ObservableCollection<SingleForecastControl>).CollectionChanged += (coll, arg) =>
                    {
                        switch (arg.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                foreach (var v in arg.NewItems)
                                    (b as BindableStackLayout).Children.Add((SingleForecastControl)v);
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                foreach (var v in arg.NewItems)
                                    (b as BindableStackLayout).Children.Remove((SingleForecastControl)v);
                                break;
                            case NotifyCollectionChangedAction.Move:
                            //Do your stuff
                            break;
                            case NotifyCollectionChangedAction.Replace:
                            //Do your stuff
                            break;
                        }
                    };
                });


        public ObservableCollection<SingleForecastControl> Items
        {
            get { return (ObservableCollection<SingleForecastControl>)GetValue(ItemsProperty); }
            set {
                SetValue(ItemsProperty, value);
               
            }
        }
    }
}