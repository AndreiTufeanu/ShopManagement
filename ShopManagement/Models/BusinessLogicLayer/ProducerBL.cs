using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Models.BusinessLogicLayer
{
    public class ProducerBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Producer> ProducersList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public ProducerBL()
        {
            ProducersList = new ObservableCollection<Producer>();
        }

        public void AddMethod(object obj)
        {
            Producer producer = obj as Producer;
            if (producer != null)
            {
                if (string.IsNullOrEmpty(producer.name))
                {
                    OperationCompleted?.Invoke(this, "You have to name your producer!");
                    return;
                }
                else if (string.IsNullOrEmpty(producer.country_of_origin))
                {
                    OperationCompleted?.Invoke(this, "You have to pick country of origin!");
                    return;
                }
                try
                {
                    context.Producer.Add(producer);
                    context.SaveChanges();
                    producer.id = context.Producer.Max(item => item.id);
                    ProducersList.Add(producer);
                    OperationCompleted?.Invoke(this, $"Producer {producer.name} has been added successfully!");
                }
                catch (Exception)
                {
                    OperationCompleted?.Invoke(this, "Invalid producer name! Try another one.");
                    context.Producer.Remove(producer);
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Producer producer = obj as Producer;
            if (producer == null)
            {
                OperationCompleted?.Invoke(this, "No producer selected!");
                return;
            }
            else if (string.IsNullOrEmpty(producer.name))
            {
                OperationCompleted?.Invoke(this, "Producer name can't be null!");
                return;
            }
            else if (string.IsNullOrEmpty(producer.country_of_origin))
            {
                OperationCompleted?.Invoke(this, "Country of region can't be null!");
                return;
            }
            try
            {
                context.ModifyProducer(producer.id, producer.name, producer.country_of_origin);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, "The producer name was changed successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid producer!");
            }
        }

        public void DeleteMethod(object obj)
        {
            Producer producer = obj as Producer;
            if (producer == null)
            {
                OperationCompleted?.Invoke(this, "You have to select a product type!");
            }
            else
            {
                context.DeactivateProducer(producer.id);
                context.SaveChanges();
                ProducersList.Remove(producer);
                OperationCompleted?.Invoke(this, $"Producer {producer.name} removed successfully!");
            }
        }

        public ObservableCollection<Producer> GetAllProducers()
        {
            return new ObservableCollection<Producer>(context.Producer.ToList());
        }
    }
}
