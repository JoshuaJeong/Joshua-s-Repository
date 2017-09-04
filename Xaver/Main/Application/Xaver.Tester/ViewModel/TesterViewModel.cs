using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Xaver.GLOBAL.COM.Helper;

namespace Xaver.Tester
{
    public class TesterViewModel
    {
        private ICommand mCommander;
        public ICommand BtnCommand
        {
            get
            {
                return mCommander;
            }
            set
            {
                mCommander = value;
            }
        }

        public TesterViewModel()
        {
            BtnCommand = new Commander(new Action<object>(Btn_Click));
        }

        private void Btn_Click(object obj)
        {
            try
            {
                Control ctl = obj as Control;

                //Logger.WriteLog(XmlSerializer<T>.Serialize(obj));
            }
            catch (Exception e)
            {
                Logger.WriteLog(null, null, "Tester", "Btn_Click", "Error", );
            }
        }


        #region Command Class
        private class Commander : ICommand
        {
            /// <summary>
            /// 명령에 따라 실행될 캡슐화 된 메소드 입니다.
            /// </summary>
            private Action<object> _action;
            /// <summary>
            /// 명령에 대한 생성자 입니다.
            /// </summary>
            /// <param name="action"></param>
            public Commander(Action<object> action)
            {
                _action = action;
            }

            /// <summary>
            /// 현재 상태에서 명령을 실행할 수 있는지 여부를 확인하는 메서드를 정의합니다.
            /// </summary>
            /// <param name="parameter">명령에서 사용하는 데이터입니다.명령에서 데이터를 전달할 필요가 없으면 이 개체를 null로 설정할 수 있습니다.</param>
            /// <returns>이 명령을 실행할 수 있으면 true이고, 그렇지 않으면 false입니다.</returns>
            public bool CanExecute(object parameter)
            {
                return true;
            }

            /// <summary>
            /// 명령을 실행해야 하는지 여부에 영향을 주는 변경이 발생할 때 발생합니다.
            /// </summary>
            public event EventHandler CanExecuteChanged;

            /// <summary>
            /// 명령이 호출될 때 호출할 메서드를 정의합니다.
            /// </summary>
            /// <param name="parameter">명령에서 사용하는 데이터입니다.명령에서 데이터를 전달할 필요가 없으면 이 개체를 null로 설정할 수 있습니다.</param>
            public void Execute(object parameter)
            {
                if (parameter != null)
                {
                    _action(parameter);
                }
                else
                {
                    //_action("");
                }
            }
        }
        #endregion
    }
}
