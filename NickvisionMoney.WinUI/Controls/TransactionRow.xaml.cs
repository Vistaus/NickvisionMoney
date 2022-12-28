using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using NickvisionMoney.Shared.Helpers;
using NickvisionMoney.Shared.Models;
using NickvisionMoney.WinUI.Helpers;
using System;
using Windows.UI;

namespace NickvisionMoney.WinUI.Controls;

/// <summary>
/// A row to display a Transaction model
/// </summary>
public sealed partial class TransactionRow : UserControl
{
    private readonly Transaction _transaction;

    /// <summary>
    /// The Id of the Transaction the row represents
    /// </summary>
    public uint Id => _transaction.Id;

    /// <summary>
    /// Occurs when the edit button on the row is clicked 
    /// </summary>
    public event EventHandler<uint>? EditTriggered;
    /// <summary>
    /// Occurs when the delete button on the row is clicked 
    /// </summary>
    public event EventHandler<uint>? DeleteTriggered;

    /// <summary>
    /// Constructs a TransactionRow
    /// </summary>
    /// <param name="transaction">The Transaction model to represent</param>
    /// <param name="defaultColor">The default transaction color</param>
    /// <param name="localizer">The Localizer for the app</param>
    public TransactionRow(Transaction transaction, Color defaultColor, Localizer localizer)
    {
        InitializeComponent();
        _transaction = transaction;
        //Localize Strings
        //Localize Strings
        ToolTipService.SetToolTip(BtnEdit, localizer["Edit", "TransactionRow"]);
        ToolTipService.SetToolTip(BtnDelete, localizer["Delete", "TransactionRow"]);
        //Load Transaction
        BtnId.Content = _transaction.Id;
        BtnId.Background = new SolidColorBrush(ColorHelpers.FromRGBA(_transaction.RGBA) ?? defaultColor);
        BtnId.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        LblName.Text = _transaction.Description;
        LblDescription.Text = _transaction.Date.ToString("d");
        if(_transaction.RepeatInterval != TransactionRepeatInterval.Never)
        {
            LblDescription.Text += $"\nRepeat Interval: {_transaction.RepeatInterval}";
        }
        LblAmount.Text = $"{(_transaction.Type == TransactionType.Income ? "+" : "-")}  {_transaction.Amount.ToString("C")}";
        LblAmount.Foreground = _transaction.Type == TransactionType.Income ? new SolidColorBrush(ActualTheme == ElementTheme.Light ? Color.FromArgb(255, 38, 162, 105) : Color.FromArgb(255, 143, 240, 164)) : new SolidColorBrush(ActualTheme == ElementTheme.Light ? Color.FromArgb(255, 192, 28, 40) : Color.FromArgb(255, 255, 123, 99));
    }

    /// <summary>
    /// Occurs when the edit button on the row is clicked 
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">RoutedEventArgs</param>
    private void Edit(object sender, RoutedEventArgs e) => EditTriggered?.Invoke(this, Id);

    /// <summary>
    /// Occurs when the delete button on the row is clicked 
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">RoutedEventArgs</param>
    private void Delete(object sender, RoutedEventArgs e) => DeleteTriggered?.Invoke(this, Id);
}