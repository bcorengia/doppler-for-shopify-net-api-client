﻿using Newtonsoft.Json.Linq;
using System.Net.Http;
using Doppler.Shopify.ApiClient.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doppler.Shopify.ApiClient.Infrastructure;

namespace Doppler.Shopify.ApiClient
{
    /// <summary>
    /// A service for manipulating Shopify orders.
    /// </summary>
    public class OrderService : ShopifyService
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrderService" />.
        /// </summary>
        /// <param name="myShopifyUrl">The shop's *.myshopify.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public OrderService(string myShopifyUrl, string shopAccessToken) : base(myShopifyUrl, shopAccessToken) { }

        /// <summary>
        /// Gets a count of all of the shop's orders.
        /// </summary>
        /// <param name="filter">Options for filtering the count.</param>
        /// <returns>The count of all orders for the shop.</returns>
        public virtual int Count(OrderFilter filter = null)
        {
            var req = PrepareRequest("orders/count.json");

            if (filter != null)
            {
                req.QueryParams.AddRange(filter.ToParameters());
            }

            return ExecuteRequest<int>(req, HttpMethod.Get, rootElement: "count");
        }

        /// <summary>
        /// Gets a list of up to 250 of the shop's orders.
        /// </summary>
        /// <param name="options">Options for filtering the list.</param>
        /// <returns>The list of orders matching the filter.</returns>
        public virtual List<Order> List(OrderFilter options = null)
        {
            var req = PrepareRequest("orders.json");

            if (options != null)
            {
                req.QueryParams.AddRange(options.ToParameters());
            }

            return ExecuteRequest<List<Order>>(req, HttpMethod.Get, rootElement: "orders");
        }

        /// <summary>
        /// Gets a list of up to 250 of the customer's orders.
        /// </summary>
        /// <param name="customerId">The id of the customer to list orders for.</param>
        /// <param name="options">Options for filtering the list.</param>
        /// <returns>The list of orders matching the filter.</returns>
        public virtual List<Order> ListForCustomer(long customerId, OrderFilter options = null)
        {
            var req = PrepareRequest("orders.json");
            req.QueryParams.Add("customer_id", customerId);

            if (options != null)
            {
                req.QueryParams.AddRange(options.ToParameters());
            }

            return ExecuteRequest<List<Order>>(req, HttpMethod.Get, rootElement: "orders");
        }

        /// <summary>
        /// Retrieves the <see cref="Order"/> with the given id.
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="Order"/>.</returns>
        public virtual Order Get(long orderId, string fields = null)
        {
            var req = PrepareRequest(string.Format("orders/{0}.json", orderId));

            if (string.IsNullOrEmpty(fields) == false)
            {
                req.QueryParams.Add("fields", fields);
            }

            return ExecuteRequest<Order>(req, HttpMethod.Get, rootElement: "order");
        }

        /// <summary>
        /// Closes an order.
        /// </summary>
        /// <param name="id">The order's id.</param>
        public virtual Order Close(long id)
        {
            var req = PrepareRequest(string.Format("orders/{0}/close.json", id));

            return ExecuteRequest<Order>(req, HttpMethod.Post, rootElement: "order");
        }

        /// <summary>
        /// Opens a closed order.
        /// </summary>
        /// <param name="id">The order's id.</param>
        public virtual Order Open(long id)
        {
            var req = PrepareRequest(string.Format("orders/{0}/open.json", id));

            return ExecuteRequest<Order>(req, HttpMethod.Post, rootElement: "order");
        }

        /// <summary>
        /// Creates a new <see cref="Order"/> on the store.
        /// </summary>
        /// <param name="order">A new <see cref="Order"/>. Id should be set to null.</param>
        /// <param name="options">Options for creating the order.</param>
        /// <returns>The new <see cref="Order"/>.</returns>
        public virtual Order Create(Order order, OrderCreateOptions options = null)
        {
            var req = PrepareRequest("orders.json");
            var body = order.ToDictionary();

            if (options != null)
            {
                foreach (var option in options.ToDictionary())
                {
                    body.Add(option);
                }
            }

            var content = new JsonContent(new
            {
                order = body
            });

            return ExecuteRequest<Order>(req, HttpMethod.Post, content, "order");
        }

        /// <summary>
        /// Updates the given <see cref="Order"/>.
        /// </summary>
        /// <param name="orderId">Id of the object being updated.</param>
        /// <param name="order">The <see cref="Order"/> to update.</param>
        /// <returns>The updated <see cref="Order"/>.</returns>
        public virtual Order Update(long orderId, Order order)
        {
            var req = PrepareRequest(string.Format("orders/{0}.json", orderId));
            var content = new JsonContent(new
            {
                order = order
            });

            return ExecuteRequest<Order>(req, HttpMethod.Put, content, "order");
        }

        /// <summary>
        /// Deletes an order with the given Id.
        /// </summary>
        /// <param name="orderId">The order object's Id.</param>
        public virtual void Delete(long orderId)
        {
            var req = PrepareRequest(string.Format("orders/{0}.json", orderId));

            ExecuteRequest(req, HttpMethod.Delete);
        }

        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <param name="orderId">The order's id.</param>
        /// <returns>The cancelled <see cref="Order"/>.</returns>
        public virtual void Cancel(long orderId, OrderCancelOptions options = null)
        {
            var req = PrepareRequest(string.Format("orders/{0}/cancel.json", orderId));
            var content = new JsonContent(options ?? new OrderCancelOptions());

            ExecuteRequest(req, HttpMethod.Post, content);
        }
    }
}