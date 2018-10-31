namespace BusinessLogic.Model
{
    public class ParameterMetadata
    {
        public string Name { get; set; }
        public TypeMetadata Type { get; set; }

        public ParameterMetadata(string name, TypeMetadata type)
        {
            Name = name;
            Type = type;
        }
    }
}