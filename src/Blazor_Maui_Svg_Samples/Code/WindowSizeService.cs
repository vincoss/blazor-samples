using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Blazor_Maui_Svg_Samples.Code
{
    public interface IWindowSize : INotifyPropertyChanged
    {
        int Width { get; }

        int Height { get; }

        bool IsPortrait { get; }

        void Resize(int width, int height);
    }

    public class WindowSizeService : IWindowSize
    {
        private int _width;
        private int _height;

        public int Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public int Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public bool IsPortrait { get { return Height > Width; } }

        public void Resize(int width, int height)
        {
            this._width = width;
            this._height = height;
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
