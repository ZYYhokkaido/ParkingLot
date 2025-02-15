namespace ParkingLotTest
{
    using ParkingLot;
    using System;
    using System.Net.Sockets;
    using Xunit;
    using Xunit.Sdk;

    public class ParkingLotTest
    {
        [Fact]
        public void Should_return_parking_ticket_When_park_Given_parking_lot_and_car()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car();
            var ticket = parkingLot.Park(car);

            Assert.NotNull(ticket);
        }

        [Fact]
        public void Should_return_parked_car_When_fetch_Given_parking_lot_with_parked_car_and_parking_ticket()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car();
            var ticket = parkingLot.Park(car);
            var fetchedCar = parkingLot.Fetch(ticket);

            Assert.Equal(car, fetchedCar);
        }

        [Fact]
        public void Should_return_two_parked_cars_When_fetch_Given_parking_lot_with_two_parked_cars_and_two_parking_tickets()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car();
            var ticket1 = parkingLot.Park(car1);
            var car2 = new Car();
            var ticket2 = parkingLot.Park(car2);
            var fetchedCar1 = parkingLot.Fetch(ticket1);
            var fetchedCar2 = parkingLot.Fetch(ticket2);

            Assert.Equal(car1, fetchedCar1);
            Assert.Equal(car2, fetchedCar2);
        }

        [Fact]
        public void Should_return_error_message_When_fetch_Given_a_wrong_ticket()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car();
            parkingLot.Park(car);
            var wrongTicket = new Ticket();
            WrongTicketException exception = Assert.Throws<WrongTicketException>(() => parkingLot.Fetch(wrongTicket));

            Assert.Equal("Unrecognized parking ticket.", exception.Message);
        }

        [Fact]
        public void Should_return_error_message_When_fetch_Given_used_a_ticket()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car();
            var ticket = parkingLot.Park(car);
            parkingLot.Fetch(ticket); // Simulate fetching the car
            WrongTicketException exception = Assert.Throws<WrongTicketException>(() => parkingLot.Fetch(ticket)); // Attempt to fetch with the same ticket

            Assert.Equal("Unrecognized parking ticket.", exception.Message);
        }

        [Fact]
        public void Should_return_error_message_When_park_Given_a_full_parkingLot()
        {
            var parkingLot = new ParkingLot(1); // Parking lot with capacity of 1
            var car1 = new Car();
            var car2 = new Car();
            parkingLot.Park(car1); // Park first car

            NoPositionException exception = Assert.Throws<NoPositionException>(() => parkingLot.Park(car2)); // Attempt to fetch with the same ticket

            Assert.Equal("No available position.", exception.Message);
        }
    }
}
