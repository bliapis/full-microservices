﻿using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.UseCases.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}