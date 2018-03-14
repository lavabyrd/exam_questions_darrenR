print("#" * 35)
print("WELCOME TO THE DBS CONSOLE")
print("#" * 35)
print("")
print("")


def start():
    print("Accept a grade and display the equivalent description:")
    print("-" * 55)
    print("")
    # takes the inputted value, converts it to upper to allow earier parsing
    grade = input("Input your grade :").upper()
    descr = grade_parse(grade)
    print("You have chosen :  " + descr)


def grade_parse(grade):
    if grade == "E":
        return "Excellent"
    elif grade == "V":
        return "Very Good"
    elif grade == "G":
        return "Good"
    elif grade == "A":
        return "Average"
    elif grade == "F":
        return "Fail"
    else:
        return "An invalid choice, please enter either E, V, G, A or F"


start()
