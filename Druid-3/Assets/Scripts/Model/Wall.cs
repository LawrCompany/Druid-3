using Interface;


namespace Model
{
    public sealed class Wall : BaseObjectScene, ISelectObject
    {
        public string GetMessage()
        {
            return Name;
        }
    }
}