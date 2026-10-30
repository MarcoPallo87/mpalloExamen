namespace mpalloExamen.Views;

public partial class vRegistro : ContentPage
{
    public string UsuarioConectado { get; set; } = string.Empty;

    // Variables para el Resumen
    public double MontoInicialGuardado { get; set; }
    public double PagoMensualGuardado { get; set; }
    public double PagoTotalGuardado { get; set; }

    public vRegistro()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lblUsuarioConectado.Text = $"Usuario conectado: {UsuarioConectado}";

        dtpFechaInicio.MinimumDate = DateTime.Today;
        dtpFechaInicio.Date = DateTime.Today;

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEdad.Text = string.Empty;
            pckPlan.SelectedIndex = -1;
            swtCasillero.IsToggled = false;
            txtMontoInicial.Text = string.Empty;
            txtPagoMensual.Text = string.Empty;
        }
    }

    private async void OnCalcularClicked(object? sender, EventArgs e)
    {
        // 1. Validar campos vacíos
        if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) ||
            string.IsNullOrWhiteSpace(txtEdad.Text) || string.IsNullOrWhiteSpace(txtMontoInicial.Text) ||
            pckPlan.SelectedItem == null)
        {
            await DisplayAlertAsync("Error", "Debe llenar todos los campos y seleccionar un plan.", "OK");
            return;
        }

        // 2. Validar Edad
        if (!int.TryParse(txtEdad.Text, out int edad) || edad < 16)
        {
            await DisplayAlertAsync("Error", "La edad debe ser un número mayor o igual a 16.", "OK");
            return;
        }

        // 3. Validar Monto Inicial
        if (!double.TryParse(txtMontoInicial.Text.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double montoInicial)
            || montoInicial < 120 || montoInicial > 600)
        {
            await DisplayAlertAsync("Error", "El monto inicial debe estar entre 120 y 600.", "OK");
            return;
        }

        // 4. CÁLCULOS
        double costoMembresia = 1200;
        double saldoRestante = costoMembresia - montoInicial;
        double cuotaBase = saldoRestante / 5;
        double recargoPorcentaje = costoMembresia * 0.02;

        double recargoCasillero = swtCasillero.IsToggled ? 10 : 0;

        // Pago mensual final
        double pagoMensual = cuotaBase + recargoPorcentaje + recargoCasillero;

        // Pago total
        double pagoTotal = montoInicial + (pagoMensual * 5);

        // Guardamos variables para el resumen
        MontoInicialGuardado = montoInicial;
        PagoMensualGuardado = pagoMensual;
        PagoTotalGuardado = pagoTotal;

        // Mostramos el resultado en el Entry bloqueado
        txtPagoMensual.Text = pagoMensual.ToString("F2");
    }

    private async void OnVerResumenClicked(object? sender, EventArgs e)
    {
        // Validar el pago primero
        if (string.IsNullOrWhiteSpace(txtPagoMensual.Text))
        {
            await DisplayAlertAsync("Atención", "Primero debe calcular el pago mensual.", "OK");
            return;
        }

        // Navegar al resumen
        var paginaResumen = new vResumen
        {
            UsuarioConectado = this.UsuarioConectado,
            Nombre = txtNombre.Text,
            Apellido = txtApellido.Text,
            Edad = txtEdad.Text,
            FechaInicio = ((DateTime)dtpFechaInicio.Date!).ToString("dd/MM/yyyy"),
            Plan = pckPlan.SelectedItem.ToString(),
            Casillero = swtCasillero.IsToggled ? "Sí" : "No",
            MontoInicial = MontoInicialGuardado,
            PagoMensual = PagoMensualGuardado,
            PagoTotal = PagoTotalGuardado
        };

        await Navigation.PushAsync(paginaResumen);
    }
}