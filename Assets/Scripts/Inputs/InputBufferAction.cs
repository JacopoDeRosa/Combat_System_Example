namespace Inputs
{
    public class InputBufferAction
    {
        private const int ActionLifetime = 5;

        private string _controlName;
        private int _lifetime = ActionLifetime;
        
        public string ControlName => _controlName;
        
        public InputBufferAction(string controlName)
        {
            _controlName = controlName;
        }

        public bool ActionTick()
        {
            return _lifetime-- <= 0;
        }
    }
}