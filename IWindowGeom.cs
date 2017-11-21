
namespace Notes
{
    public interface IWindowGeom
    {
        int[] LoadConfig();
        void SaveConfig(int x, int y, int w, int h);
    }
}
