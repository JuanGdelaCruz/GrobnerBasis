using System;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Color = Microsoft.Msagl.Drawing.Color;
using Node = Microsoft.Msagl.Drawing.Node;
using Shape = Microsoft.Msagl.Drawing.Shape;
using GröbnerBasis.PolynomialRings;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using GröbnerBasis.PolynomialRings.Order;
using GröbnerBasis.PolynomialRings.Fields;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Editing
{
    public partial class Form1 : Form
    {

        private CancellationTokenSource _cts;

        public Form1()
        {

            graphEditor = new GraphEditor();
            InitializeComponent();
            graphEditor.AddNodeType("Ellipse", Shape.Ellipse, Color.Transparent, Color.Black, 10, "user data", "New Node");
            graphEditor.AddNodeType("Square", Shape.Box, Color.Transparent, Color.Black, 6, "user data", "");
            graphEditor.AddNodeType("Double Circle", Shape.DoubleCircle, Color.Transparent, Color.Black, 6, "user data", "New Node");
            graphEditor.AddNodeType("Diamond", Shape.Diamond, Color.Transparent, Color.Black, 6, "user data", "New Node");
            graphEditor.Viewer.NeedToCalculateLayout = true;
            CreateGraph();
            graphEditor.Viewer.NeedToCalculateLayout = false;
            SuspendLayout();
       
            graphEditor.Viewer.LayoutAlgorithmSettingsButtonVisible = false;
            splitContainer1.Panel1.Controls.Add(graphEditor);
            graphEditor.Dock = DockStyle.Fill;
            ResumeLayout();

        }


        private void CreateGraph()
        {
            var g = new Graph();

            //Default graph
            g.AddEdge("x1", "x2").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x1", "x5").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x1", "x6").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x2", "x8").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x2", "x3").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x2", "x4").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x3", "x8").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x3", "x4").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x4", "x5").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x4", "x7").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x5", "x7").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x5", "x6").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x6", "x7").Attr.ArrowheadAtTarget = ArrowStyle.None;
            g.AddEdge("x7", "x8").Attr.ArrowheadAtTarget = ArrowStyle.None;

            graphEditor.Graph = g;

        }




        private async void CheckButton_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";

            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            int k = (int)numericUpDown1.Value;
            progressBar1.Visible = true;
            CheckButton.Visible = false;
            cancel.Visible = true;

            var progressHandler = new Progress<int>(value =>
            {
                progressBar1.Value = value;
            });

            var progress = progressHandler as IProgress<string>;
            try
            {
                var result = await Task.Run(() =>
                {
                    return Check(k,token, progressHandler);                    
                 });
                foreach (Polynomial p in result.Item1)
                    textBox2.Text += p.ToString() + "\r\n";

                foreach (Polynomial p in result.Item2)
                    textBox3.Text += p.ToString() + "\r\n";
                textBox1.Text = (result.Item3 ? "The graph is " + k + "-colorable." : "The graph is not " + k + "-colorable.");

            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Cancelled.");
               
            }
            progressBar1.Visible = false;
            cancel.Visible = false;
            CheckButton.Visible = true;

        }



        private  Tuple<Polynomial[],Polynomial [],bool> Check(int k,CancellationToken token,IProgress<int> progress)
        {
            var graph = graphEditor.Graph;
            var variables = graph.Nodes.Select(x => x.Label.Text).ToArray();
            foreach (String s in variables)
                Console.WriteLine(s);
            Ring ring = new Ring(Field.Real, variables);
            ring.FixOrder(variables);

            List<Polynomial> generator = new List<Polynomial>();
            List<Node> nodes = graph.Nodes.ToList();

            float currentProgress = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                int[] powerProduct = new int[graph.NodeCount];
                powerProduct[i] = k;
                Polynomial first = new Polynomial(ring);
                first.AddTerm(1, powerProduct);
                first.AddTerm(-1, new int[nodes.Count]);

                generator.Add(first);

                var node = nodes[i];
                foreach (Edge edge in node.Edges)
                {

                    Polynomial second = new Polynomial(ring);

                    int index = nodes.IndexOf(edge.TargetNode);
                    if (index == i)
                        continue;
                    powerProduct = new int[nodes.Count];
                    powerProduct[i] = k - 1;
                    second.AddTerm(1, powerProduct);
                    // xi^k−1 + xj^k−2 * xj +· · ·+xi* xj^k−2 + xj^k−1
                    for (int j = k - 2; j > 0; j--)
                    {
                        powerProduct = new int[nodes.Count];
                        powerProduct[i] = j;
                        powerProduct[index] = k - 1 - j;
                        second.AddTerm(1, powerProduct);

                    }
                    powerProduct = new int[nodes.Count];
                    powerProduct[i] = 0;
                    powerProduct[index] = k - 1;
                    second.AddTerm(1, powerProduct);

                    generator.Add(second);
                }
                currentProgress = i / nodes.Count *100;
                progress.Report((int)currentProgress);
            }
             

            Ideal ideal = new Ideal(generator.ToArray(), ring);
            Polynomial one = new Polynomial(ring);
            one.AddTerm(1, new int[nodes.Count]);
            var reducedBasis = ideal.ReducedGrobnerBasis(token,progress);

            return new Tuple<Polynomial[],Polynomial[],bool> (ideal.GeneratorSet,reducedBasis, !reducedBasis.Any(i => i.Equals(one)));
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            CheckButton.Text = "Comprobar";

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}