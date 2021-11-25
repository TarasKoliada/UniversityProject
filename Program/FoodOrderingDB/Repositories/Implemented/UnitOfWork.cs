using FoodOrderingDB.Repositories.Implemented;

namespace FoodOrderingDB.Repositories
{
    class UnitOfWork
    {
        private OrderingContext _orderingContext = new OrderingContext();

        private CustomerRepository _customerRepo;
        private OrderRepository _orderRepo;
        private SiteRepository _siteRepo;
        private EmployeeRepository _employeeRepo;
        private PaymentRepository _paymentRepo;
        private RatingRepository _ratingRepo;
        private AdminRepository _adminRepo;

        public CustomerRepository Customers 
        {
            get
            {
                if (_customerRepo == null)
                    _customerRepo = new CustomerRepository(_orderingContext);

                return _customerRepo;
            }
        }

        public OrderRepository Orders
        {
            get 
            {
                if (_orderRepo == null)
                    _orderRepo = new OrderRepository(_orderingContext);

                return _orderRepo;
            }
        }

        public SiteRepository Sites
        {
            get
            {
                if (_siteRepo == null)
                    _siteRepo = new SiteRepository(_orderingContext);

                return _siteRepo;
            }
        }

        public EmployeeRepository Employees
        {
            get
            {
                if (_employeeRepo == null)
                    _employeeRepo = new EmployeeRepository(_orderingContext);
               
                return _employeeRepo;
            }
        }

        public PaymentRepository Payments
        {
            get 
            {
                if (_paymentRepo == null)
                    _paymentRepo = new PaymentRepository(_orderingContext);

                return _paymentRepo;
            }
        }

        public RatingRepository Ratings
        {
            get 
            {
                if (_ratingRepo == null)
                    _ratingRepo = new RatingRepository(_orderingContext);

                return _ratingRepo;
            }
        }

        public AdminRepository Admins
        {
            get
            {
                if (_adminRepo == null)
                    _adminRepo = new AdminRepository(_orderingContext);

                return _adminRepo;
            }
        }

    }
}
