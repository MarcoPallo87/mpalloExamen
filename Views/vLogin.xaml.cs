namespace mpalloExamen.Views;

public partial class vLogin : ContentPage
{
    public vLogin()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        string usuario = txtUsuario.Text?.Trim() ?? "";
        string contrasena = txtContrasena.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
        {
            await DisplayAlertAsync("Error", "Debe ingresar usuario y contraseña.", "OK");
            return;
        }

        // Matriz de usuarios
        string[,] usuarios = {
            { "recepción", "gym2026" },
            { "entrenador", "fit_01" },
            { "uisrael", "2026" },
            { "gerente", "admin123" }
        };

        bool accesoConcedido = false;

        // Matriz para validar
        for (int i = 0; i < 4; i++)
        {
            if (usuarios[i, 0] == usuario && usuarios[i, 1] == contrasena)
            {
                accesoConcedido = true;
                break;
            }
        }

        if (accesoConcedido)
        {
            var paginaRegistro = new vRegistro();
            paginaRegistro.UsuarioConectado = usuario;
            await Navigation.PushAsync(paginaRegistro);
        }
        else
        {
            await DisplayAlertAsync("Error", "Datos incorrectos. Verifique sus credenciales.", "OK");
        }
    }

    private async void OnAcercaDeClicked(object? sender, EventArgs e)
    {
        // Mostramos la alerta con mis datos
        await DisplayAlertAsync("Acerca de", "App Desarrollada por: Marco Pallo López", "OK");
    }
}