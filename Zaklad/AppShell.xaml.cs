﻿using Zaklad.Views;

namespace Zaklad
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(@$"{nameof(MainPage)}/{nameof(AddItemByName)}", typeof(AddItemByName));
            Routing.RegisterRoute(@$"{nameof(MainPage)}/{nameof(AddItemByBarcode)}", typeof(AddItemByBarcode));
            Routing.RegisterRoute(@$"{nameof(MainPage)}/{nameof(CreateCustomProduct)}", typeof(CreateCustomProduct));
        }
    }
}