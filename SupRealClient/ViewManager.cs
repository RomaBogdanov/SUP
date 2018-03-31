using SupRealClient.Common.Interfaces;
using SupRealClient.Models;
using SupRealClient.Views;

namespace SupRealClient
{
    // TODO - подумать, как реализовать более грамотно

    public class ViewManager : IViewManager
    {
        public static IViewManager viewManager;

        public static IViewManager Instance
        {
            get { return viewManager = viewManager ?? new ViewManager(); }
        }

        public void Add(IAddItem1Model model)
        {
            AddItem1View addItem1View = new AddItem1View(model);
            addItem1View.Show();
        }

        // Пока криво - потом переделать
        public void AddObject(object model)
        {
            if (model is AddOrgsModel)
            {
                AddUpdateOrgsView orgsView = new AddUpdateOrgsView(model as AddOrgsModel);
                orgsView.Show();
            }
            else if (model is AddCardModel)
            {
                AddUpdateCardView cardView = new AddUpdateCardView(model as AddCardModel);
                cardView.Show();
            }
        }

        public void Update(IAddItem1Model model)
        {
            AddItem1View addItem1View = new AddItem1View(model);
            addItem1View.Show();
        }

        // Пока криво - потом переделать
        public void UpdateObject(object model)
        {
            if (model is UpdateOrgsModel)
            {
                AddUpdateOrgsView orgsView = new AddUpdateOrgsView(model as UpdateOrgsModel);
                orgsView.Show();
            }
            else if (model is UpdateCardModel)
            {
                AddUpdateCardView cardView = new AddUpdateCardView(model as UpdateCardModel);
                cardView.Show();
            }
        }

        public void Search(ISearchHelper searchHelper)
        {
            Search1View search1View = new Search1View(searchHelper);
            search1View.Show();
        }
    }
}
