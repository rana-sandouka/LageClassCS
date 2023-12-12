	public class ValidateOrder : IValidateOrder
	{
		public bool OrderValidation(OrderManager orderManager, World world, int clientId, Order order)
		{
			if (order.Subject == null || order.Subject.Owner == null)
				return true;

			var subjectClientId = order.Subject.Owner.ClientIndex;
			var subjectClient = orderManager.LobbyInfo.ClientWithIndex(subjectClientId);

			if (subjectClient == null)
			{
				Log.Write("debug", "Order sent to {0}: resolved ClientIndex `{1}` doesn't exist", order.Subject.Owner.PlayerName, subjectClientId);
				return false;
			}

			var isBotOrder = subjectClient.Bot != null && clientId == subjectClient.BotControllerClientIndex;

			// Drop orders from players who shouldn't be able to control this actor
			// This may be because the owner changed within the last net tick,
			// or, less likely, the client may be trying to do something malicious.
			if (subjectClientId != clientId && !isBotOrder)
				return false;

			return order.Subject.AcceptsOrder(order.OrderString);
		}
	}