﻿namespace Application.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int productId)
            : base("Product", productId)
        {

        }
    }
}
