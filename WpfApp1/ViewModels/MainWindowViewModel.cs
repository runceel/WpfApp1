using Reactive.Bindings;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Notifiers;
using System.Diagnostics;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : Bases.ViewModelBase
    {
        public ReadOnlyReactiveProperty<CellViewModel> CurrentCell { get; }

        public ReactiveCommand SetCurrentCellCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ClearCurrentCellCommand { get; } = new ReactiveCommand();
        public ReactiveCommand TestCommand { get; }

        public MainWindowViewModel()
        {
            CurrentCell = Observable.Merge(
                SetCurrentCellCommand
                    .Select(_ => new CellViewModel { IsFirstFlag = (!CurrentCell.Value?.IsFirstFlag) ?? false }),
                ClearCurrentCellCommand
                    .Select(_ => default(CellViewModel))
            ).ToReadOnlyReactiveProperty();

            TestCommand = CurrentCell.Select(x => x?.IsFirstFlag ?? false)
                .ToReactiveCommand()
                .WithSubscribe(() => Debug.WriteLine(CurrentCell.Value.IsFirstFlag));
        }
    }
}
