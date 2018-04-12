using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _LFP_Proyecto1
{
    public partial class Form1 : Form
    {

        //----- Variables de entrada -----
        private string consolaText, prompt = "jbgr# ";
        private int consolaLinea = 1;
        private int consolaFila = 0, idLexemaInicial = 0;
        private int cursor, inicioFila;
        private string rutaRaiz, rutaRaizE;

        //----- Terminales -----
        string[] L = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "\xD1", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "\xF1", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        string[] N = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        string[] G = { "-", "_" };
        string[] C = { "\""};
        string[] D = { "/", "\\" };
        string[] SL = { "\n" };

        //----- Token -----
        string[,] token = new string[,] {
                { "ID", "Token", "Descripcion" },
                { "1", "Id", "Cadena de numeros y letras" },
                { "2", "CrearCarpeta", "Palabra reservada, instrucción" },
                { "3", "renombrarC", "Palabra reservada, instrucción" },
                { "4", "AbrirC", "Palabra reservada, instrucción" },
                { "5", "regresar", "Palabra reservada, instrucción" },
                { "6", "eliminarC", "Palabra reservada, instrucción" },
                { "7", "CrearArchivo", "Palabra reservada, instrucción" },
                { "8", "abrirArchivo", "Palabra reservada, instrucción" },
                { "9", "renombrarA", "Palabra reservada, instrucción" },
                { "10", "mover", "Palabra reservada, instrucción" },
                { "11", "copiar", "Palabra reservada, instrucción" },
                { "12", "eliminarA", "Palabra reservada, instrucción" },
                { "13", "cargar", "Palabra reservada, instrucción" },
                { "14", "Ejecutar", "Palabra reservada, instrucción" },
                { "15", "ManualUsuario", "Palabra reservada, instrucción" },
                { "16", "ManualTecnico", "Palabra reservada, instrucción" },
                { "17", "AcercaDe", "Palabra reservada, instrucción" },
                { "18", "Ruta", "Cadena Path" },
                { "19", "verReportes", "Palabra reservada, instrucción" }
            };

        //----- Palabras reservadas -----
        string[,] palabrasReservadas = new string[,] {
                { "2", "CrearCarpeta" },
                { "3", "renombrarC" }, 
                { "4", "AbrirC" }, 
                { "5", "regresar" }, 
                { "6", "eliminarC" }, 
                { "7", "CrearArchivo" },
                { "8", "abrirArchivo" }, 
                { "9", "renombrarA" }, 
                { "10", "mover" }, 
                { "11", "copiar" }, 
                { "12", "eliminarA" }, 
                { "13", "cargar" }, 
                { "14", "Ejecutar" }, 
                { "15", "ManualUsuario" }, 
                { "16", "ManualTecnico" },
                { "17", "acercaDe" },
                { "19", "verReportes"}
            };
        
        
        //----- Constructor ----
        public Form1()
        {
            InitializeComponent();
            IniciarArbol();
            Arbol.ImageList = Iconos;
            Arbol.SelectedNode = Arbol.Nodes[0];
            EscribirPrompt();
        }

        //----- Comparar ----
        private Boolean comparar(string[] matrizDeCaracteres, string caracter)
        {
            string caracterStr = caracter.ToString();
            for (int i = 0; i < matrizDeCaracteres.Length; i++)
            {
                if (caracterStr == matrizDeCaracteres[i])
                {
                    return true;
                }
            }
            return false;
        }

        //  ----- Analizador lexico ----
        private void AnalizarLenguaje(string cadena)
        {
            Console.WriteLine("PRESIONASTE EL BOTON");
            int indice = 0;
            int estadoActual = 0;
            char caracterActual;
            string lexema = "";

            int colActual = 0, filaActual = 1, fila = 0, columna = 0;

            //----- Inicio de interacciones ----
            for (indice = 0; indice < cadena.Length; indice++)
            {
                caracterActual = cadena[indice];
                string caracterActualStr = caracterActual.ToString();
                colActual++;

                //----- AFD ----
                switch (estadoActual)
                {
                    case 0: //----- Estado Inicial ----
                        fila = filaActual;
                        columna = colActual;
                        if (comparar(L, caracterActualStr))
                        {
                            lexema += caracterActual;
                            estadoActual = 1;
                        }
                        else if (comparar(N, caracterActualStr))
                        {
                            lexema += caracterActual;
                            estadoActual = 1;
                        }
                        else if (comparar(C, caracterActualStr))
                        {
                            lexema += caracterActual;
                            estadoActual = 2;
                        }
                        else
                        {
                            switch (caracterActual)
                            {
                                case ' ':
                                case '\t':
                                case '\b':
                                case '\f':
                                case '\r':
                                    estadoActual = 0;
                                    break;
                                case '\n':
                                    filaActual++;
                                    colActual = 0;
                                    estadoActual = 0;
                                    break;
                                default:
                                    lexema += caracterActual;
                                    estadoActual = -1;
                                    colActual--;
                                    break;
                            }
                        }
                        break;
                    case 1:     //  ----- Estado 1 -----
                        if (comparar(N, caracterActualStr))
                        {

                            estadoActual = 1;
                            lexema += caracterActual;
                        }
                        else if (comparar(L, caracterActualStr))
                        {
                            estadoActual = 1;
                            lexema += caracterActual;
                        }
                        else if (comparar(G, caracterActualStr))
                        {
                            estadoActual = 1;
                            lexema += caracterActual;
                        }
                        else if (!comparar(N, caracterActualStr))
                        {
                            //----- Estado de aceptacion -----
                            ValidarLexema(lexema, consolaLinea, columna, "Id");
                            estadoActual = 0;
                            lexema = "";
                            indice--;
                            colActual--;

                        }
                        break;
                    case 2:     //  ----- Estado 2 -----
                        if (comparar(N, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else if (comparar(L, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else
                        {
                            estadoActual = -1;
                            indice--;
                        }
                        break;
                    case 3:     //  ----- Estado 3 -----
                        if (comparar(N, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else if (comparar(L, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else if (comparar(G, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else if (comparar(D, caracterActualStr))
                        {
                            estadoActual = 4;
                            lexema += caracterActual;
                        }
                        else if (comparar(C, caracterActualStr))
                        {
                            estadoActual = 5;
                            lexema += caracterActual;
                        }
                        else
                        {
                            estadoActual = -1;
                            indice--;
                        }
                        break;
                    case 4:     //  ----- Estado 4 -----
                        if (comparar(N, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else if (comparar(L, caracterActualStr))
                        {
                            estadoActual = 3;
                            lexema += caracterActual;
                        }
                        else if (comparar(C, caracterActualStr))
                        {
                            estadoActual = 5;
                            lexema += caracterActual;
                        }
                        else
                        {
                            estadoActual = -1;
                            indice--;
                        }
                        break;
                    case 5:     //  ----- Estado 5 -----
                        //----- Estado de aceptacion -----
                        ValidarLexema(lexema, consolaLinea, columna, "Ruta");
                        estadoActual = 0;
                        lexema = "";
                        indice--;
                        colActual--;
                        break;
                    default: //  ----- Error -----
                        AgregarError(lexema, consolaLinea, columna);
                        indice--;
                        estadoActual = 0;
                        lexema = "";
                        break;
                }
            }
        }

        List<lexema> tablaDeSimbolos = new List<lexema>();
        List<error> tablaDeErrores = new List<error>();
        List<archivo> listaDeArchivos = new List<archivo>();
        int num = 0;
        int numError = 1;

        private void AgregarArchivo(string nombre, string codigo)
        {
            listaDeArchivos.Add(new archivo() {nombre = nombre, codigo =codigo});
        }

        private void AgregarLexema(string idToken, string lexema, int fila, int columna, string token)
        {
            tablaDeSimbolos.Add(new lexema() { id = num, idToken = idToken, nombre = lexema, fila = fila, columna = columna, token = token });
            num++;
            
        }
        private void AgregarError(string caracter, int fila, int columna)
        {
            tablaDeErrores.Add(new error() { id = numError, caracter = caracter, fila = fila, columna = columna });
            numError++;
        }

        private string ValidarLexema(string lexema, int fila, int columna, string tipo)
        {

            // tokens y palabras reservadas
            if (tipo == "Ruta")   //    ---- Si viene ruta -----
            {
                lexema = lexema.Replace(" ", "");
                AgregarLexema(token[18, 0], lexema, fila, columna, token[18, 2]);
                return "+ TOKEN: " + lexema + "\t(Fila: " + fila + ", Col: " + columna + ")" + "\tId Token: " + token[1, 0] + "\tToken: " + token[1, 2];

            }
            else if (tipo == "Id")   //   ---- Si vienen ID o instrucciones: -----
            {
                lexema = lexema.Replace(" ", "");
                for (int i = 0; i < palabrasReservadas.GetLength(0); i++)
                {
                    if (lexema == palabrasReservadas[i, 1])
                    {
                        int id = Int32.Parse(palabrasReservadas[i, 0]);
                        AgregarLexema(token[id, 0], lexema, fila, columna, token[id, 2]);
                        return "+ TOKEN: " + lexema + "\t(Fila: " + fila + ", Col: " + columna + ")" + "\tId Token: " + token[id, 0] + "\tToken: " + token[id, 2];

                    }
                }
                AgregarLexema(token[1, 0], lexema, fila, columna, token[1, 2]);
                return "+ TOKEN: " + lexema + "\t(Fila: " + fila + ", Col: " + columna + ")" + "\tId Token: " + token[2, 0] + "\tToken: " + token[2, 2];

            }

            return "ERROR INESPERADO...";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string rutaTablaDeSimbolos = " ";
        string rutaTablaDeErrores = "";

        private void GenerarTablaDeErrores()
        {

            string html = "<center><h1 style=\'text-align: center;\'>KTurtle</h1><h2 style=\'text-align: center;\'>Tabla de errores:</h2><hr /><p>​&nbsp;</p><table style=\'width: 800px;\' border=\'1\' cellspacing=\'1\' cellpadding=\'1\'><thead><tr><th scope=\'col\'><span style=\'color: #000000;\'>#</span></th><th scope=\'col\'><span style=\'color: #000000;\'>Fila</span></th>"
            + "<th scope=\'col\'><span style=\'color: #000000;\'>Columna</span></th><th scope=\'col\'><span style=\'color: #000000;\'>Caracter</span></th>"
            + "<th scope=\'col\'><span style=\'color: #000000;\'>Id Descripcion</span>"
            + "</tr></thead><tbody>";

            for (int i = 0; i < tablaDeErrores.Count; i++)
            {
                string lexemas = "<tr><td style=\'text-align: center;\'> " + tablaDeErrores[i].id + "</td><td style=\'text-align: center;\'> " + tablaDeErrores[i].fila + "</td><td style=\'text-align: center;\'>" + tablaDeErrores[i].columna + "</td><td style=\'text-align: center;\'>" + tablaDeErrores[i].caracter + "</td><td>Desconocido</td></tr>";
                html += lexemas;
            }

            html += "</tbody></table><p>&nbsp;</p><hr /><p>&nbsp;</p></center>";

            SaveFileDialog saveFileDialog1;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Generar tabla de errores";
            saveFileDialog1.Filter = "Archivo de html (.html) |*.html";

            saveFileDialog1.DefaultExt = "html";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = @"H:\LO DEL ESCRITORIO";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rutaTablaDeErrores = saveFileDialog1.FileName;

                StreamWriter fichero = new StreamWriter(rutaTablaDeErrores);
                fichero.Write(html);
                fichero.Close();
                Process.Start(rutaTablaDeErrores);
            }
            else
            {
                saveFileDialog1.Dispose();
                saveFileDialog1 = null;
            }

        }

        private void GenerarTablaDeSimbolos()
        {

            string html = "<center><h1 style=\'text-align: center;\'>KTurtle</h1><h2 style=\'text-align: center;\'>Tabla de simbolos:</h2><hr /><p>​&nbsp;</p><table style=\'width: 800px;\' border=\'1\' cellspacing=\'1\' cellpadding=\'1\'><thead><tr><th scope=\'col\'><span style=\'color: #000000;\'>#</span></th><th scope=\'col\'><span style=\'color: #000000;\'>Lexema</span></th>"
            + "<th scope=\'col\'><span style=\'color: #000000;\'>Fila</span></th><th scope=\'col\'><span style=\'color: #000000;\'>Columna</span></th>"
            + "<th scope=\'col\'><span style=\'color: #000000;\'>Id Token</span></th><th scope=\'col\'><span style=\'color: #000000;\'>Token</span></th>"
            + "</tr></thead><tbody>";

            for (int i = 0; i < tablaDeSimbolos.Count; i++)
            {
                string lexemas = "<tr><td style=\'text-align: center;\'> " + tablaDeSimbolos[i].id + "</td><td style=\'text-align: center;\'> " + tablaDeSimbolos[i].nombre + "</td><td style=\'text-align: center;\'>" + tablaDeSimbolos[i].fila + "</td><td style=\'text-align: center;\'>" + tablaDeSimbolos[i].columna + "</td><td style=\'text-align: center;\'>" + tablaDeSimbolos[i].idToken + "</td><td>" + tablaDeSimbolos[i].token + "</td></tr>";
                html += lexemas;
            }

            html += "</tbody></table><p>&nbsp;</p><hr /><p>&nbsp;</p></center>";

            SaveFileDialog saveFileDialog1;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Generar tabla de simbolos";
            saveFileDialog1.Filter = "Archivo de html (.html) |*.html";

            saveFileDialog1.DefaultExt = "html";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = @"H:\LO DEL ESCRITORIO";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rutaTablaDeSimbolos = saveFileDialog1.FileName;

                StreamWriter fichero = new StreamWriter(rutaTablaDeSimbolos);
                fichero.Write(html);
                fichero.Close();
                Process.Start(rutaTablaDeSimbolos);
            }
            else
            {
                saveFileDialog1.Dispose();
                saveFileDialog1 = null;
            }

        }


       
        private void button1_Click(object sender, EventArgs e)
        {
            GenerarTablaDeErrores();
            GenerarTablaDeSimbolos();
        }

        private void Consola_TextChanged(object sender, EventArgs e)
        {

        }

        //----- Escribe el prompt -----
        private void EscribirPrompt()
        {
            Consola.Text = Consola.Text + prompt;
            Consola.SelectionStart = Consola.Text.Length;
            Consola.ScrollToCaret();
        }

        //----- Cuando se presiona Enter -----
        private void Consola_KeyPress(object o, KeyPressEventArgs e)
        {

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string cadena = Consola.Text.Substring(inicioFila,  Consola.Text.Length - inicioFila);

                idLexemaInicial = tablaDeSimbolos.Count;
                AnalizarLenguaje(cadena + "\n");
                Ejecutar();
                consolaLinea++;
                EscribirPrompt();

            }
            cursor = Consola.SelectionStart;
            inicioFila = Consola.Text.LastIndexOf("\n") + prompt.Length;
        }

        private void Arbol_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void EscribirEnConsola(string texto)
        {
            Consola.Text += texto+"\n";
        }


        //----- Ejecutar -----

        private void Ejecutar()
        {
            int estadoInicial = 0;
            int estadoActual = 0;
            string tokenactual;
            string tokenNombreN = "", tokenNombreA = "";
            string tokenAuxiliar = "";
            string expresion = "";
            string comando = " ";
            string tId = "";
            string x = "", y = "";
            AgregarLexema("error", "error", 0, 0, "error");

            for (estadoInicial = idLexemaInicial; estadoInicial < tablaDeSimbolos.Count; estadoInicial++)
            {
                tokenactual = tablaDeSimbolos[estadoInicial].nombre;
                tId = tablaDeSimbolos[estadoInicial].idToken;

                switch (estadoActual)
                {
                    case 0:
                        if (tId == "2" || tId == "4" || tId == "6" || tId == "7" || tId == "8" || tId == "12" || tId == "14")
                        {
                            estadoActual = 1;
                            comando += tokenactual + " ";
                            tokenAuxiliar = tId;
                        }
                        else if (tId == "3" || tId == "9" || tId == "10" || tId == "11" || tId == "13")
                        {
                            estadoActual = 2;
                            comando += tokenactual + " ";
                            tokenAuxiliar = tId;

                        }
                        else if (tId == "19")
                        {
                            EscribirEnConsola("Generando reportes... ");
                            tablaDeSimbolos.RemoveAt(tablaDeSimbolos.Count - 1);
                            GenerarTablaDeErrores();
                            GenerarTablaDeSimbolos();
                            EscribirEnConsola("Abriendo reportes... ");
                        }
                        else if (tId == "5")
                        {
                            regresar();
                        }
                        else if (tId == "15")
                        {
                            string pdfPath = Path.Combine(Application.StartupPath, "Documentos\\Manual de usuario.pdf");
                            Process.Start(pdfPath);
                            EscribirEnConsola("Abriendo manual de usuario... ");
                        }
                        else if (tId == "16")
                        {
                            string pdfPath = Path.Combine(Application.StartupPath, "Documentos\\Manual tecnico.pdf");
                            Process.Start(pdfPath);
                            EscribirEnConsola("Abriendo manual tecnico... ");
                        }
                        else if (tId == "17")
                        {
                            string info = "Autor: Josué Benjamín Girón Ramírez\nCarné: 201318631\r\n\nUniversidad San Carlos de Guatemala\r\n\nPrimer semestre del año 2018\nCurso: LENGUAJES FORMALES Y DE PROGRAMACION\nCódigo: 796 \nSección: A- \nEdificio: T3 \nSalón: 214 \r\n\nCopyright © Todos los Derechos Reservados.";
                            MessageBox.Show(info);
                            EscribirEnConsola("Acerca de ...");
                        }
                        else { estadoActual = 500; }

                        break;
                    case 1:
                        if (tId == "1")
                        {
                            comando += tokenactual + " ";
                            tokenNombreN = tokenactual;
                            if (tokenAuxiliar == "2")
                            {
                                CrearCarpeta(tokenNombreN);
                            }
                            else if (tokenAuxiliar == "4")
                            {
                                AbrirC(tokenNombreN);
                            }
                            else if (tokenAuxiliar == "6")
                            {
                                eliminarC(tokenNombreN);
                            }
                            else if (tokenAuxiliar == "7")
                            {
                                CrearArchivo(tokenNombreN);
                            }
                            else if (tokenAuxiliar == "8")
                            {
                                abrirArchivo(tokenNombreN);
                            }
                            else if (tokenAuxiliar == "12")
                            {
                                eliminarA(tokenNombreN);
                            }
                            else if (tokenAuxiliar == "14")
                            {
                                abrirArchivo(tokenNombreN);
                                EscribirEnConsola("Se ha ejecutado \"" + tokenNombreN + "\"");
                            }
                            estadoActual = 0;
                            tokenNombreN = " ";
                            expresion = " ";
                            comando = " ";
                        }
                        else
                        {
                            estadoActual = 500;
                            estadoInicial--;
                        }
                        break;
                    case 2:
                        if (tId == "1")
                        {
                            estadoActual = 3;
                            comando += tokenactual + " ";
                            tokenNombreA = tokenactual;
                        }
                        else if (tId == "18")
                        {
                            estadoActual = 3;
                            comando += tokenactual + " ";
                            tokenNombreA = tokenactual;
                        }
                        else
                        {
                            estadoActual = 500;
                            estadoInicial--;
                        }
                        break;
                    case 3:
                        if (tId == "1")
                        {
                            comando += tokenactual + " ";
                            tokenNombreN = tokenactual;
                            if (tokenAuxiliar == "3")
                            {
                                renombrarC(tokenNombreA, tokenNombreN);
                            }
                            else if (tokenAuxiliar == "9")
                            {
                                renombrarA(tokenNombreA, tokenNombreN);
                            }
                            else if (tokenAuxiliar == "13")
                            {
                                cargar(tokenNombreN, tokenNombreA);
                                
                            }
                            estadoActual = 0;
                            tokenNombreN = " ";
                            expresion = " ";
                            comando = " ";
                        }
                        else if (tId == "18")
                        {
                            comando += tokenactual + " ";
                            tokenNombreN = tokenactual;
                            if (tokenAuxiliar == "10")
                            {
                                moverOCopiar(tokenNombreA, tokenNombreN, "mover");
                            }
                            else if (tokenAuxiliar == "11")
                            {
                                moverOCopiar(tokenNombreA, tokenNombreN, "copiar");
                            }
                            estadoActual = 0;
                            tokenNombreN = " ";
                            expresion = " ";
                            comando = " ";
                        }
                        else
                        {
                            estadoActual = 500;
                            estadoInicial--;
                        }
                        break;

                    case 500:

                        estadoInicial = 500;
                        if (!(comando == " "))
                        {
                            EscribirEnConsola("La instruccion esta incompleta...\r\n \"" + comando + "\"");
                        }
                        else
                        {
                            EscribirEnConsola("Se espera una o mas instrucciones validas...");
                        }
                        comando = " ";
                        break;
                    }
                }
            tablaDeSimbolos.RemoveAt(tablaDeSimbolos.Count-1);
             }
        
        public void CrearCarpeta(string nomCarpeta)
        {
            if (BuscarNodo(nomCarpeta, "carpeta") != null)
            {
                EscribirEnConsola("Error: No se puede crear la carpeta \""+ nomCarpeta + "\" debido que ya existe...");
            }
            else
            {
                EscribirCMD("mkdir " + "\"" + rutaRaiz + "\\" + nomCarpeta + "\"");
                Arbol.SelectedNode.Nodes.Add("carpeta", nomCarpeta, 1,2);
                Arbol.SelectedNode.Expand();
                EscribirEnConsola("Carpeta\"" + nomCarpeta + "\" creada");
            }

        }

        public void renombrarC(string nomCarpetaA, string nomCarpetaN)
        {
            TreeNode nodo = BuscarNodo(nomCarpetaA, "carpeta");
            if (nodo == null)
            {
                EscribirEnConsola("Error: No se puede renombrar la carpeta \"" + nomCarpetaA + "\" debido que no existe...");
            }
            else
            {
                EscribirCMD("move /Y " + "\"" + rutaRaiz + "\\" + nomCarpetaA + "\" " + "\"" + rutaRaiz + "\\" + nomCarpetaN + "\"");
                nodo.Text = nomCarpetaN;
                EscribirEnConsola("Carpeta \"" + nomCarpetaA + "\" renombrada a \"" + nomCarpetaN + "\"");
            }

        }

        public void renombrarA(string nomArchivoA, string nomArchivoN)
        {
            TreeNode nodo = BuscarNodo(nomArchivoA, "archivo");
            if (nodo == null)
            {
                EscribirEnConsola("Error: No se puede renombrar el archivo \"" + nomArchivoA + "\" debido que no existe...");
            }
            else
            {
                EscribirCMD("ren " + "\"" + rutaRaiz + "\\" + nomArchivoA + ".lfp\" " + nomArchivoN + ".lfp");
                nodo.Text = nomArchivoN;
                EscribirEnConsola("Archivo \"" + nomArchivoA + "\" renombrado a \"" + nomArchivoN + "\"");
            }

        }

        public void moverOCopiar(string nomArchivo, string rutaN, string funcion)
        {
            TreeNode nodo = BuscarNodo(nomArchivo, "archivo");
            rutaN = rutaN.Replace("/", "\\");
            int ultimo = rutaN.LastIndexOf("/") + 1 ;
            string nodoX = rutaN.Substring(ultimo, rutaN.Length - ultimo-1);
            if (nodo == null)
            {
                EscribirEnConsola("Error: No se puede mover el archivo \"" + nomArchivo + "\" debido que no existe...");
            }
            else
            {
                if (funcion == "mover") 
                {
                    nodo.Remove();
                }
                
                
                TreeNode[] nodos = Arbol.Nodes.Find("carpeta", true);
                for (int i = 0; i < nodos.Length; i++)
                {
                    string rutaNodo = "\"" + nodos[i].FullPath + "\"";
                    if (rutaNodo == rutaN )
                    {
                        rutaN = nodos[i].FullPath.Substring(5, nodos[i].FullPath.Length - 5);
                        if (funcion == "mover")
                        {
                            EscribirCMD("move " + "\"" + rutaRaiz + "\\" + nomArchivo + ".lfp\" " + "\"" + rutaRaizE + "\\" + rutaN + "\"");
                            EscribirEnConsola("Archivo \"" + nomArchivo + "\" se mueve a \"" + rutaN + "\"");
                        }
                        else if(funcion == "copiar")
                        {
                            EscribirCMD("copy " + "\"" + rutaRaiz + "\\" + nomArchivo + ".lfp\" " + "\"" + rutaRaizE + "\\" + rutaN + "\\" + nomArchivo + ".lfp\"");
                            EscribirEnConsola("Archivo \"" + nomArchivo + "\" se copia a \"" + rutaN + "\"");
                        }
                        nodos[i].Nodes.Add("archivo", nomArchivo, 3, 3);
                    }
                }
                
                
            }

        }

        public void AbrirC(String nomCarpeta)
        {
            TreeNode nodo = BuscarNodo(nomCarpeta, "carpeta");
            if (nodo != null)
            {
                rutaRaiz = rutaRaiz + "\\" + nomCarpeta;
                Arbol.SelectedNode = nodo;
                Arbol.SelectedNode.Expand();
                EscribirEnConsola("Se ha entrado a la carpeta \"" + nomCarpeta + "\"");
            }
            else
            {
                EscribirEnConsola("No se puede acceder al directorio \"" + nomCarpeta + "\" o no existe...");
            }
            
        }

        public void CrearArchivo(String nomArchivo)
        {
            if (BuscarNodo(nomArchivo, "archivo") != null)
            {
                EscribirEnConsola("Error: No se puede crear el archivo \"" + nomArchivo + "\" debido que ya existe...");
            }
            else
            {
                EscribirCMD("goto >> " + "\"" + rutaRaiz + "\\" + nomArchivo + ".lfp\"");
                Arbol.SelectedNode.Nodes.Add("archivo", nomArchivo, 3, 3);
                Arbol.SelectedNode.Expand();
                EscribirEnConsola("Archivo\"" + nomArchivo + "\" creado");
            }

        }

        
        public void abrirArchivo(String nomArchivo)
        {
            if (BuscarNodo(nomArchivo, "archivo") == null)
            {
                EscribirEnConsola("Error: No se puede abrir El carpeta \"" + nomArchivo + "\" debido que ya existe...");
            }
            else
            {
                Process.Start(rutaRaiz + "\\" + nomArchivo + ".lfp");
                EscribirEnConsola("Abriendo \"" + nomArchivo + ".lfp\" ...");
            }

        }
        public TreeNode BuscarNodo(string textNodo, string tipo)
        {
            TreeNode nodo = Arbol.SelectedNode;
            for (int i = 0; i < nodo.Nodes.Count; i++)
            {
                if (nodo.Nodes[i].Text == textNodo && nodo.Nodes[i].Name == tipo)
                {
                    return nodo.Nodes[i];
                }
            }
            return null;
        }
        

        public void eliminarC(String nomCarpeta)
        {
            TreeNode nodo = BuscarNodo(nomCarpeta, "carpeta");
            if (nodo == null)
            {
                EscribirEnConsola("Error: No se puede eliminar la carpeta \"" + nomCarpeta + "\" debido que no existe...");
            }
            else
            {
                EscribirCMD("rmdir /s /q " + "\"" + rutaRaiz + "\\" + nomCarpeta + "\"");
                nodo.Remove();

                
                EscribirEnConsola("Carpeta \"" + nomCarpeta + "\" Eliminada");
            }
        }
        public void cargar(string nombre, string direccion)
        {
            direccion = direccion.Replace("\"", "");
            direccion = direccion.Replace("/", "\\");

            try
            {
                AgregarArchivo(nombre, System.IO.File.ReadAllText(direccion + "\\" + nombre + ".lfp"));
                EscribirEnConsola("Se carga el archivo \"" + nombre + "\" de " + direccion + "");
            }
            catch
            {
                EscribirEnConsola("Archivo no existe...");
            }
        }

        public void eliminarA(String nomArchivo)
        {
            TreeNode nodo = BuscarNodo(nomArchivo, "archivo");
            if (nodo == null)
            {
                EscribirEnConsola("Error: No se puede eliminar el Archivo \"" + nomArchivo + "\" debido que no existe...");
            }
            else
            {
                EscribirCMD("del /Q  " + "\"" + rutaRaiz + "\\" + nomArchivo + ".lfp\"");
                nodo.Remove();


                EscribirEnConsola("Archivo \"" + nomArchivo + "\" eliminado");
            }
        }


        public void regresar()
        {
            
            if (Arbol.SelectedNode == Arbol.Nodes[0])
            {
                EscribirEnConsola("No se puede regresar mas...");
            }
            else
            {
                int ultimo=rutaRaiz.LastIndexOf("\\"); ;
                rutaRaiz = rutaRaiz.Remove(ultimo, rutaRaiz.Length - ultimo);
                TreeNode nodo = Arbol.SelectedNode;
                Arbol.SelectedNode.Collapse(true);
                Arbol.SelectedNode = nodo.Parent;
                //Arbol.SelectedNode.Expand();
                EscribirEnConsola("Se regresa..." + rutaRaiz);
            }
        }
       
        public string EscribirCMD(string comando)
        {

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(comando);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return cmd.StandardOutput.ReadToEnd();
            
            
        }

        Process cmd = new Process();
        private void IniciarArbol()
        {
            
            FolderBrowserDialog dlCarpeta = new FolderBrowserDialog();
            dlCarpeta.RootFolder = System.Environment.SpecialFolder.Desktop;
            dlCarpeta.ShowNewFolderButton = false;
            dlCarpeta.Description = "Selecciona la carpeta Raiz";
            if (dlCarpeta.ShowDialog() == DialogResult.OK)
            {
                rutaRaiz = dlCarpeta.SelectedPath;
                rutaRaizE = rutaRaiz;
                EscribirEnConsola("Carpeta Raiz: " + rutaRaiz);
                this.Focus();
            }
            
        }

    }
    
    
}

public class lexema
{
    public int hola;
    public int id { get; set; }
    public string idToken { get; set; }
    public string nombre { get; set; }
    public int fila { get; set; }
    public int columna { get; set; }
    public string token { get; set; }
}


public class error
{
    public int id { get; set; }
    public int fila { get; set; }
    public int columna { get; set; }
    public string caracter { get; set; }

}

public class archivo
{
    public string nombre { get; set; }
    public string codigo { get; set; }

}