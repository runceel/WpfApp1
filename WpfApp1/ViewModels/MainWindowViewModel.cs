using Reactive.Bindings;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings.Extensions;

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
            {
                var flag = false;
                if(CurrentCell.Value != null)
                {
                    flag = !CurrentCell.Value.IsFirstFlag;
                }
                var curCell = new CellViewModel() { IsFirstFlag = flag };
                CurrentCell.Value = curCell;
            });

            ClearCurrentCellCommand.Subscribe(_ => CurrentCell.Value = null);

            var testProp = new[]
{
                CurrentCell.Select(a => a!=null),
                CurrentCell.Where(a => a != null).Select(a => a.IsFirstFlag),
                // CurrentCell.Select(a => a != null && a.IsFirstFlag), ★ こっちだと正しく動作する
            }.CombineLatestValuesAreAllTrue().ToReactiveProperty();
            testProp.Subscribe(a => System.Console.WriteLine($"来たよ{a}"));

            TestCommand = new[]
            {
                CurrentCell.Select(a => a!=null),
                CurrentCell.Where(a => a != null).Select(a => a.IsFirstFlag),
                // CurrentCell.Select(a => a != null && a.IsFirstFlag), ★ こっちだと正しく動作する
            }.CombineLatestValuesAreAllTrue().ToReactiveCommand();
            TestCommand.Subscribe(_ => System.Console.WriteLine(CurrentCell.Value.IsFirstFlag));
        }
        
    }
}
