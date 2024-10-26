namespace Practica_10
{
    public partial class Form1 : Form
    {
        List<string> estacionamiento = Enumerable.Repeat("Vacio", 11).ToList();
        static Random random = new Random();
        int intervaloEntrada;
        int intervaloSalida;
        public Form1()
        {
            InitializeComponent();
            //Seleccion de intervalo random
            int[] intervalos = { 500, 1000, 2000 };
            intervaloEntrada = intervalos[random.Next(intervalos.Length)];
            intervaloSalida = intervalos[random.Next(intervalos.Length)];
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Crear y empezar el hilo que cambia de "Vacio" a "Ocupado"
            Thread add = new Thread(AddList);
            add.Start();

            // Crear y empezar el hilo que cambia de "Ocupado" a "Vacio"
            Thread substract = new Thread(SubstractList);
            substract.Start();
        }

        private void SubstractList()
        {
            velocidadSalida.Invoke((MethodInvoker)delegate
            {
                float imprirmirIntervalo = intervaloSalida / 1000f;
                velocidadSalida.Text = imprirmirIntervalo.ToString("0.0");
            });
            while (true)
            {
                
                //Lista con indices ocupados que cambiaran a vacios
                List<int> indicesOcupados = new List<int>();
                //Iteramos lista
                for (int i = 0; i < estacionamiento.Count; i++)
                {
                    if (estacionamiento[i] == "Ocupado")
                    {
                        indicesOcupados.Add(i);
                    }
                }

                //Revisamos poder sacar autos
                if (indicesOcupados.Count > 0)
                {
                    int indiceAleatorio = indicesOcupados[random.Next(indicesOcupados.Count)];
                    estacionamiento[indiceAleatorio] = "Vacio";
                }

                // Usar Invoke para actualizar la interfaz de usuario desde el hilo principal
                listBox1.Invoke((MethodInvoker)delegate
                {
                    listBox1.Items.Clear();
                    foreach (string e in estacionamiento)
                    {
                        listBox1.Items.Add(e);
                    }
                });
                Thread.Sleep(intervaloSalida);
            }
        }

        private void AddList()
        {
            velocidadEntrada.Invoke((MethodInvoker)delegate
            {
                float imprimirIntervalo = intervaloEntrada / 1000f;
                velocidadEntrada.Text = imprimirIntervalo.ToString("0.0");
            });
            while (true)
            {
                
                //Lista de indices vacios que cambiaran a ocupados
                List<int> indicesVacios = new List<int>();

                //Iterar la lista booleana
                for (int i = 0; i < estacionamiento.Count; i++)
                {
                    if (estacionamiento[i] == "Vacio")
                    {
                        indicesVacios.Add(i);
                    }
                }

                if (indicesVacios.Count > 0)
                {
                    int indiceAleatorio = indicesVacios[random.Next(indicesVacios.Count)];
                    estacionamiento[indiceAleatorio] = "Ocupado";
                }

                // Usar Invoke para actualizar la interfaz de usuario desde el hilo principal
                listBox1.Invoke((MethodInvoker)delegate
                {
                    listBox1.Items.Clear();
                    foreach (string e in estacionamiento)
                    {
                        listBox1.Items.Add(e);
                    }
                });
                Thread.Sleep(intervaloEntrada);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indiceSeleccionado = comboBox1.SelectedIndex;
            switch (indiceSeleccionado)
            {
                case 0:
                    intervaloEntrada = 500;
                    break;
                case 1:
                    intervaloEntrada = 1000;
                    break;
                case 2:
                    intervaloEntrada = 2000;
                    break;
            }
            float imprimirIntervalo = intervaloEntrada / 1000f;
            velocidadEntrada.Text = imprimirIntervalo.ToString("0.0");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int indiceSeleccionado = comboBox2.SelectedIndex;
            switch (indiceSeleccionado)
            {
                case 0:
                    intervaloSalida = 500;
                    break;
                case 1:
                    intervaloSalida = 1000;
                    break;
                case 2:
                    intervaloSalida = 2000;
                    break;
            }
            float imprimirIntervalo = intervaloSalida / 1000f;
            velocidadSalida.Text = imprimirIntervalo.ToString("0.0");
        }
    }
}
