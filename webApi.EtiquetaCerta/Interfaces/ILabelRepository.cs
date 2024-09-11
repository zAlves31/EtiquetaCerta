using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILabelRepository
    {
        public void Register(Label label);

        public List<Label> List();

        public Label GetById(Guid id);
    }
}
