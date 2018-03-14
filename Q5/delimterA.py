print("#" * 35)
print("WELCOME TO THE DBS CONSOLE")
print("#" * 35)


# entry point

def start_program():
    print("Enter your data row")
    datarow = input()
    print("")
    print("Enter your delimeter")
    delim = input()
    print("")
    print("The individual data values are as follows:")
    parser(datarow, delim)


def parser(input_value, delimeter):
    valueout = input_value.split("/")
    for i in valueout:
        print(i)


start_program()
