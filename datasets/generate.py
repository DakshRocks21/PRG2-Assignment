import random

with open("datasets\\country.csv", "r") as f:
    global countries
    countries = f.readlines()
    countries = [x.strip().split(",") for x in countries]

def generate_terminal_data(terminal_number, path="datasets\\"):
    header_airlines = "Airline Name,Airline Code"
    sample_airlines = "{},{}"

    random_country_list = random.sample(countries, 100)
    random_country_list = [x for x in random_country_list if '"' not in x[1] and x[1].isascii()]
    singapore_entry = ["SIN", "Singapore"]
    random_country_list.append(singapore_entry)

    with open(f"{path}airlines_T{terminal_number}.csv", "w") as f:
        f.write(header_airlines)
        f.write("\n")
        for country in random_country_list:
            f.write(sample_airlines.format(country[1] + " Airlines", country[0]))
            f.write("\n")

    header_flight = "Flight Number,Origin,Destination,Expected Departure/Arrival,Special Request Code"
    sample_flight = "{},{},{},{},{}"
    last_option = ["", "DDJB", "LWTT", "CFFT"]

    used_flight_numbers = set()

    with open(f"{path}flights_T{terminal_number}.csv", "w") as f:
        f.write(header_flight)
        f.write("\n")
        for i in range(100):
            if random.randint(0, 1) == 0:
                origin = singapore_entry
                destination = random.choice(random_country_list)
                while destination == singapore_entry:
                    destination = random.choice(random_country_list)
            else:
                destination = singapore_entry
                origin = random.choice(random_country_list)
                while origin == singapore_entry:
                    origin = random.choice(random_country_list)

            while True:
                flight_number = f"{origin[0]} {random.randint(100, 999)}"
                if flight_number not in used_flight_numbers:
                    used_flight_numbers.add(flight_number)
                    break

            origin_airport = f"{origin[1]} ({origin[0]})"
            destination_airport = f"{destination[1]} ({destination[0]})"

            hour = random.randint(1, 12) 
            minute = random.randint(0, 59)
            am_pm = "AM" if random.randint(0, 1) == 0 else "PM"
            time = f"{hour:02}:{minute:02} {am_pm}"

            special_request_code = random.choice(last_option)

            f.write(sample_flight.format(
                flight_number,
                origin_airport,
                destination_airport,
                time,
                special_request_code
            ))
            f.write("\n")

for i in range(1, 5):
    generate_terminal_data(i, "datasets\\")
