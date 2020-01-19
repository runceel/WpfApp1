using Reactive.Bindings;
using System.Reactive.Linq;
using System;
using System.Linq;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : Bases.ViewModelBase
    {
        public ReactiveProperty<CellViewModel> CurrentCell { get; set; } = new ReactiveProperty<CellViewModel>();

        public ReactiveCommand SetCurrentCellCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand ClearCurrentCellCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand TestCommand { get; set; }

        public MainWindowViewModel()
        {
            SetCurrentCellCommand.Subscribe(_ => 
                CurrentCell.Value =  new CellViewModel
                {
                    IsFirstFlag = !(CurrentCell.Value?.IsFirstFlag ?? true),
                }
            );

            ClearCurrentCellCommand.Subscribe(_ => CurrentCell.Value = null);

            TestCommand = CurrentCell
                .Select(x => x?.IsFirstFlag ?? false)
                .ToReactiveCommand();
            TestCommand.Subscribe(_ => Console.WriteLine(CurrentCell.Value.IsFirstFlag));
        }
        
    }
}
