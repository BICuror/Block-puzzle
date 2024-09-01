using Model;
using View;

namespace ViewModel
{
    public sealed class PartRestockerViewModel
    {
        private PartRestockerModel _partRestockerModel;
        private PartFactory _partFactory;
        private PartContainerViewContainer _partContainerViewContainer;
        private PartContainerViewModel[] _partContainerViewModels;

        public PartRestockerViewModel(PartRestockerModel partRestockerModel, PartContainerViewContainer partContainersContainer, PartFactory partFactory)
        {
            _partRestockerModel = partRestockerModel;
            _partFactory = partFactory;
            _partContainerViewContainer = partContainersContainer;
        }

        public void CreatePartContainerViewModels()
        {
            PartContainerModel[] partConainterModels = _partRestockerModel.PartContainerModels;

            PartContainerView[] _partContainers = _partContainerViewContainer.Get();  

            _partContainerViewModels = new PartContainerViewModel[partConainterModels.Length];

            for (int i = 0; i < _partContainers.Length; i++)
            {
                _partContainerViewModels[i] = new(partConainterModels[i], _partContainers[i], _partFactory);
            }
        }
    }
}

