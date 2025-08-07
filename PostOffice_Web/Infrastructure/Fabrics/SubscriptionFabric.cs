using ModelsLibrary.Infrastructure;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Positions;

namespace PostOffice.Infrastructure.Fabrics;
public static class SubscriptionFabric
{
    public static Subscription Fabric(Subscriber subscriber, Publication publication) =>
        new() { SubscriberId = subscriber.Id, StartDate = new DateTime(2021, 1, 1).AddDays(UtilsMethods.RandomValue(1, 1095)), SubscriptionCompleted = (UtilsMethods.RandomValue(0, 10) > 5 ? true : false), SubscriptionDuration = UtilsMethods.RandomValue(1, 120), PublicationId = publication.Id};
}
