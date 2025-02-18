using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using People.Models;

namespace People
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Controlador de evento asincrónico para el botón "Nuevo"
        public async void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            // Llamada asincrónica para agregar una nueva persona
            await App.PersonRepo.AddNewPerson(newPerson.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;
        }

        // Controlador de evento asincrónico para el botón "Obtener"
        public async void OnGetButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            // Llamada asincrónica para obtener todas las personas
            List<Person> people = await App.PersonRepo.GetAllPeople();
            peopleList.ItemsSource = people;
        }
    }
}
