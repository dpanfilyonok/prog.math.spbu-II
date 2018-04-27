﻿using System;
using Gtk;
using CalculatorSource;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    /// <summary>
    /// Ons the number button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnNumButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        EntryBox.Text += button.Label;
    }

    /// <summary>
    /// Ons the clear button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnClearButtonClicked(object sender, EventArgs e)
    {
        EntryBox.Text = "";
    }

    /// <summary>
    /// Ons the backspace button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnBackspaceButtonClicked(object sender, EventArgs e)
    {
        if (EntryBox.Text.Length > 0)
        {
            EntryBox.Text = EntryBox.Text.Remove(EntryBox.Text.Length - 1);
        }
    }

    /// <summary>
    /// Ons the evaluate button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnEvaluateButtonClicked(object sender, EventArgs e)
    {
        Calculate();
    }

    /// <summary>
    /// Ons the enter clicked.
    /// </summary>
    /// <param name="o">O.</param>
    /// <param name="args">Arguments.</param>
    [GLib.ConnectBefore]
    protected void OnEnterClicked(object o, KeyPressEventArgs args)
    {

        if (args.Event.Key == Gdk.Key.Return || args.Event.Key == Gdk.Key.KP_Enter)
        {
            Calculate();
        }
    }

    /// <summary>
    /// Calculate this instance.
    /// </summary>
    private void Calculate()
    {
        try
        {
            EntryBox.Text = Calculator.Evaluate(EntryBox.Text).ToString();
        }
        catch (CalculatorSource.Exceptions.InvalidExpressionException ex)
        {
            MessageBox.Show(ex.Message);
            EntryBox.Text = string.Empty;
        }
    }

    /// <summary>
    /// Message box. (Это просто скопированный из сети класс для демонстрации сообщения аля MessageBox в WinForms)
    /// </summary>
    private static class MessageBox
    {
        public static void Show(Gtk.Window parent_window, DialogFlags flags, MessageType msgtype, ButtonsType btntype, string msg)
        {
            MessageDialog md = new MessageDialog(parent_window, flags, msgtype, btntype, msg);
            md.Run();
            md.Destroy();
        }

        public static void Show(string msg)
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, msg);
            md.Run();
            md.Destroy();
        }
    }
}