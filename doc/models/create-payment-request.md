## Create Payment Request

Creates a payment from the source (nonce, card on file, etc.)

The `PAYMENTS_WRITE_ADDITIONAL_RECIPIENTS` OAuth permission is required
to enable application fees.

For more information, see [Payments and Refunds Overview](https://developer.squareup.com/docs/payments-api/overview).

For information about application fees in a payment, see
[Collect Fees](https://developer.squareup.com/docs/payments-api/take-payments-and-collect-fees).

### Structure

`CreatePaymentRequest`

### Fields

| Name | Type | Tags | Description |
|  --- | --- | --- | --- |
| `SourceId` | `string` |  | The ID for the source of funds for this payment.  This can be a nonce<br>generated by the Payment Form or a card on file made with the Customers API. |
| `IdempotencyKey` | `string` |  | A unique string that identifies this CreatePayment request. Keys can be any valid string<br>but must be unique for every CreatePayment request.<br><br>Max: 45 characters<br><br>See [Idempotency keys](https://developer.squareup.com/docs/basics/api101/idempotency) for more information. |
| `AmountMoney` | [`Models.Money`](/doc/models/money.md) |  | Represents an amount of money. `Money` fields can be signed or unsigned.<br>Fields that do not explicitly define whether they are signed or unsigned are<br>considered unsigned and can only hold positive amounts. For signed fields, the<br>sign of the value indicates the purpose of the money transfer. See<br>[Working with Monetary Amounts](https://developer.squareup.com/docs/build-basics/working-with-monetary-amounts)<br>for more information. |
| `TipMoney` | [`Models.Money`](/doc/models/money.md) | Optional | Represents an amount of money. `Money` fields can be signed or unsigned.<br>Fields that do not explicitly define whether they are signed or unsigned are<br>considered unsigned and can only hold positive amounts. For signed fields, the<br>sign of the value indicates the purpose of the money transfer. See<br>[Working with Monetary Amounts](https://developer.squareup.com/docs/build-basics/working-with-monetary-amounts)<br>for more information. |
| `AppFeeMoney` | [`Models.Money`](/doc/models/money.md) | Optional | Represents an amount of money. `Money` fields can be signed or unsigned.<br>Fields that do not explicitly define whether they are signed or unsigned are<br>considered unsigned and can only hold positive amounts. For signed fields, the<br>sign of the value indicates the purpose of the money transfer. See<br>[Working with Monetary Amounts](https://developer.squareup.com/docs/build-basics/working-with-monetary-amounts)<br>for more information. |
| `DelayDuration` | `string` | Optional | The duration of time after the payment's creation when Square automatically cancels the<br>payment. This automatic cancellation applies only to payments that don't reach a terminal state<br>(COMPLETED, CANCELED, or FAILED) before the `delay_duration` time period.<br><br>This parameter should be specified as a time duration, in RFC 3339 format, with a minimum value<br>of 1 minute.<br><br>Notes:<br>This feature is only supported for card payments. This parameter can only be set for a delayed<br>capture payment (`autocomplete=false`).<br><br>Default:<br><br>- Card Present payments: "PT36H" (36 hours) from the creation time.<br>- Card Not Present payments: "P7D" (7 days) from the creation time. |
| `Autocomplete` | `bool?` | Optional | If set to `true`, this payment will be completed when possible. If<br>set to `false`, this payment will be held in an approved state until either<br>explicitly completed (captured) or canceled (voided). For more information, see<br>[Delayed Payments](https://developer.squareup.com/docs/payments-api/take-payments#delayed-payments).<br><br>Default: true |
| `OrderId` | `string` | Optional | Associate a previously created order with this payment |
| `CustomerId` | `string` | Optional | The [Customer](#type-customer) ID of the customer associated with the payment.<br>Required if the `source_id` refers to a card on file created using the Customers API. |
| `LocationId` | `string` | Optional | The location ID to associate with the payment. If not specified, the default location is<br>used. |
| `ReferenceId` | `string` | Optional | A user-defined ID to associate with the payment.<br>You can use this field to associate the payment to an entity in an external system.<br>For example, you might specify an order ID that is generated by a third-party shopping cart.<br><br>Limit 40 characters. |
| `VerificationToken` | `string` | Optional | An identifying token generated by `SqPaymentForm.verifyBuyer()`.<br>Verification tokens encapsulate customer device information and 3-D Secure<br>challenge results to indicate that Square has verified the buyer identity.<br><br>See the [SCA Overview](https://developer.squareup.com/docs/sca-overview). |
| `AcceptPartialAuthorization` | `bool?` | Optional | If set to true and charging a Square Gift Card, a payment may be returned with<br>amount_money equal to less than what was requested.  Example, a request for $20 when charging<br>a Square Gift Card with balance of $5 wil result in an APPROVED payment of $5.  You may choose<br>to prompt the buyer for an additional payment to cover the remainder, or cancel the gift card<br>payment.  Cannot be `true` when `autocomplete = true`.<br><br>For more information, see<br>[Partial amount with Square gift cards](https://developer.squareup.com/docs/payments-api/take-payments#partial-payment-gift-card).<br><br>Default: false |
| `BuyerEmailAddress` | `string` | Optional | The buyer's e-mail address |
| `BillingAddress` | [`Models.Address`](/doc/models/address.md) | Optional | Represents a physical address. |
| `ShippingAddress` | [`Models.Address`](/doc/models/address.md) | Optional | Represents a physical address. |
| `Note` | `string` | Optional | An optional note to be entered by the developer when creating a payment<br><br>Limit 500 characters. |
| `StatementDescriptionIdentifier` | `string` | Optional | Optional additional payment information to include on the customer's card statement<br>as part of statement description. This can be, for example, an invoice number, ticket number,<br>or short description that uniquely identifies the purchase.<br><br>Note that the `statement_description_identifier` may get truncated on the statement description<br>to fit the required information including the Square identifier (SQ *) and name of the<br>merchant taking the payment. |

### Example (as JSON)

```json
{
  "idempotency_key": "4935a656-a929-4792-b97c-8848be85c27c",
  "amount_money": {
    "amount": 200,
    "currency": "USD"
  },
  "source_id": "ccof:uIbfJXhXETSP197M3GB",
  "autocomplete": true,
  "customer_id": "VDKXEEKPJN48QDG3BGGFAK05P8",
  "location_id": "XK3DBG77NJBFX",
  "reference_id": "123456",
  "note": "Brief description",
  "app_fee_money": {
    "amount": 10,
    "currency": "USD"
  }
}
```

