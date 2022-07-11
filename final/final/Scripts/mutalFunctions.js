//this script has mutual functions for several pages


//search properties with parameters that the user inserts - advanced search
function SearchProperty() {
    let dest = document.getElementById("inputDestination");
    let rooms = document.getElementById("inputRoom");
    let ReviewScore = document.getElementById("ReviewScore");
    let check_in = $("#inputCheckIn").val();
    let check_out = $("#inputCheckOut").val();
    //add the fields as the user wrote 
    let property_fill = {
        Location: dest.options[dest.selectedIndex].text,
        Bedrooms: parseInt(rooms.options[rooms.selectedIndex].value),
        Review_scores_rating: parseFloat(ReviewScore.options[ReviewScore.selectedIndex].value),
        Min_price: parseFloat($("#minPrice").val()),
        Max_price: parseFloat($("#maxPrice").val()),
        Center_distance: parseFloat($("#DistanceFromCenter").val()),
        Check_in: check_in,
        Check_out: check_out
    }

    check_in = SetDateFormat(check_in);
    check_out = SetDateFormat(check_out);

    //add the values of the search for the property that will fit the sql
    let property = {
        Location: dest.options[dest.selectedIndex].text,
        Bedrooms: parseInt(rooms.options[rooms.selectedIndex].value),
        Review_scores_rating: parseFloat(ReviewScore.options[ReviewScore.selectedIndex].value),
        Min_price: parseFloat($("#minPrice").val()),
        Max_price: parseFloat($("#maxPrice").val()),
        Center_distance: parseFloat($("#DistanceFromCenter").val()),
        Check_in: check_in,
        Check_out: check_out
    }

    localStorage.setItem("property", JSON.stringify(property));
    localStorage.setItem("property_fill", JSON.stringify(property_fill));
    window.location.href = "ViewProperties.html";
    return false;
}

//search lean properties with parameters that the user inserts
function SearchLeanProperty() {
    let dest = document.getElementById("inputDestination");
    let check_in = $("#inputCheckIn").val();
    let check_out = $("#inputCheckOut").val();

    //add the fields as the user wrote 
    let property_fill = {
        Location: dest.options[dest.selectedIndex].text, //amsterdam
        Check_in: check_in,
        Check_out: check_out
    }

    check_in = SetDateFormat(check_in);
    check_out = SetDateFormat(check_out);

    //add the values of the search for the property that will fit the sql
    let property = {
        Location: dest.options[dest.selectedIndex].text,
        Check_in: check_in,
        Check_out: check_out
    }


    localStorage.setItem("property", JSON.stringify(property));
    localStorage.setItem("property_fill", JSON.stringify(property_fill));
    window.location.href = "ViewProperties.html";
    return false;
}

//function checks if user is online - and if the admin is online - 
//and builds the navbar accordingly
function checkOnlineUser() {
    let str = "";
    //if the user is registered and logged in
    if (localStorage.getItem("user_online") != undefined) {
        let user_online = JSON.parse(localStorage.getItem("user_online"));
        let arr = user_online.fullName.split(" ");
        let name = arr[0];
        //if the user is not the admin - show the relevant navbar
        if (name.localeCompare("admin") != 0) {
            str += `<ul class="navbar-nav ml-auto">
                            <li class="nav-item">
                                <a class="nav-link">Hello, ${name}</a>
                                 </li>
                            <li class="nav-item">
                                <a class="nav-link" href="index.html" id="home_sign">Home <span class="sr-only">(current)</span></a>
                                 </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="Orders.html">My orders</a>
                                 </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="" onclick="logOut()">Log out</a>
                                 </li>
                           </ul>`;
        }
        else { //admin navbar
            str += ` <ul class="navbar-nav ml-auto">
                                <li class="nav-item">
                                    <a id="hello_id" class="nav-link">Hello, ${name}</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="UsersManager.html">Users <span class="sr-only">(current)</span></a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="HostsManager.html">Hosts</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link active" href="PropertiesManager.html">Properties</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="index.html" onclick="logOut_admin()">Log out</a>
                                </li>
                            </ul>`;
        }
   
    }

    else { //if the user is not logged in
        str += `<ul class="navbar-nav ml-auto">
                            <li class="nav-item">
                                <a class="nav-link" href="index.html" id="home_sign">Home <span class="sr-only">(current)</span></a>
                                 </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="Login.html">Login</a>
                                 </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="Register.html">Register</a>
                                 </li>
                           </ul>`;
    }
    //set the html that was chosen above
    $("#mainNav").html(str);
    let arr_pages = location.href.split("/"); 
    if (arr_pages[4].localeCompare("index.html") == 0) {
        $("#home_sign").addClass("active");
    }
}

//logout function for admin
function logOut_admin() {
    localStorage.removeItem("user_online");
    localStorage.removeItem("PropertyToSave");
    window.location.href = "index.html";
}

// log out function for regular user
function logOut() {
    localStorage.removeItem("user_online");
    location.reload();
}

//change the format of the date from check in check out fields to the sql format - yyyy/mm/dd
function SetDateFormat(date) {
    let arr = date.split(' ');
    switch (arr[1]) {
        case "Jan":
            arr[1] = "01"
            break;

        case "Feb":
            arr[1] = "02"
            break;

        case "Mar":
            arr[1] = "03"
            break;

        case "Apr":
            arr[1] = "04"
            break;

        case "May":
            arr[1] = "05"
            break;

        case "Jun":
            arr[1] = "06"
            break;

        case "Jul":
            arr[1] = "07"
            break;

        case "Aug":
            arr[1] = "08"
            break;

        case "Sep":
            arr[1] = "09"
            break;

        case "Oct":
            arr[1] = "10"
            break;

        case "Nov":
            arr[1] = "11"
            break;

        case "Dec":
            arr[1] = "12"
            break;

    }
    let date_format = arr[3] + "-" + arr[1] + "-" + arr[2];
    return date_format;
}

//advance search - all parameters - rerender the form
function advanced() {
    let str = ` <form action="" method="get" class="tm-search-form tm-section-pad-2" id="searchForm">
                                <div class="form-row tm-search-form-row">
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-1">
                                        <label for="inputCity">Choose Your Destination</label>
                                        <select name="destination" class="form-control tm-select" id="inputDestination"></select>
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-1">
                                        <div class="form-group tm-form-group tm-form-group-pad tm-form-group-2">
                                            <label for="inputRoom">How many rooms?</label>
                                            <select name="room" class="form-control tm-select" id="inputRoom">
                                                <option value="1" selected>1 Room</option>
                                                <option value="2">2 Rooms</option>
                                                <option value="3">3 Rooms</option>
                                                <option value="4">4 Rooms</option>
                                                <option value="5">5 Rooms</option>
                                                <option value="6">6 Rooms</option>
                                                <option value="7">7 Rooms</option>
                                                <option value="8">8 Rooms</option>
                                                <option value="9">9 Rooms</option>
                                                <option value="10">10 Rooms</option>
                                            </select>
                                        </div>
                                        <div class="form-group tm-form-group tm-form-group-pad tm-form-group-1">
                                            <label for="ReviewScore">Review Score</label>
                                            <select name="ReviewScore" class="form-control tm-select" id="ReviewScore">
                                                <option value="1" selected>1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                            </select>
                                        </div>
                                    </div>
                                </div> <!-- form-row -->
                                <div class="form-row tm-search-form-row">
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-3">
                                        <label for="inputMinPrice">Min Price</label>
                                        <input name="minPrice" type="text" class="form-control" id="minPrice" placeholder="Min price" required>
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-3">
                                        <label for="inputMaxPrice">Max Price</label>
                                        <input name="maxPrice" type="text" class="form-control" id="maxPrice" placeholder="Max price" required>
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-2">
                                        <label for="DistFromCenter">Distance from the center</label>
                                        <br />
                                        <input class ="slider" type="range" name="ageInputName" id="DistanceFromCenter" value="5" min="0" max="15" oninput="DistanceFromCenterOut.value = DistanceFromCenter.value +'  Km'">
                                        <output name="ageOutputName" id="DistanceFromCenterOut">5 KM</output>
                                    </div>
                                </div>   <!-- form-row -->
                                <div class="form-row tm-search-form-row">
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-3">
                                        <label for="inputCheckIn">Check In Date</label>
                                        <input name="check-in" type="text" class="form-control" id="inputCheckIn" placeholder="Check In" required onkeydown="event.preventDefault()">
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-3">
                                        <label for="inputCheckOut">Check Out Date</label>
                                        <input name="check-out" type="text" class="form-control" id="inputCheckOut" placeholder="Check Out" required onkeydown="event.preventDefault()">
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group">
                                        <button type="submit" class="btn btn-primary tm-btn tm-btn-search text-uppercase" id="btnSubmit">Check Availability</button>
                                        <div class="form-group tm-form-group tm-form-group-pad tm-form-group">
                                            <button type ="button" class="btn btn-primary tm-btn pull-right" onclick="lean()">-</button>
                                        </div>
                                    </div>
                                </div>
                            </form>`;

    $("#tm-section-search").html(str);
    setDestinations();
    pickerCheckIn = datepicker('#inputCheckIn');
    pickerCheckOut = datepicker('#inputCheckOut');

    $("#searchForm").submit(function () {
        let check_in = $("#inputCheckIn").val();
        let check_out = $("#inputCheckOut").val();

        check_in = SetDateFormat(check_in);
        check_out = SetDateFormat(check_out);
        let check_in_Date = new Date(check_in);
        let check_out_Date = new Date(check_out);
        let today = new Date();
        if (today.getTime() > check_in_Date.getTime()) {
            swal("Error picking dates", "Check in cannot be before today", "error");
            return false;
        }
        if (check_in_Date.getTime() > check_out_Date.getTime()) {
            swal("Error picking dates", "Check out cannot be before check in", "error");
            return false;
        }
        SearchProperty();
        return false;
    });

    if (localStorage.getItem("property_fill") != undefined)
        fillSearchForm();

}

//lean search - render search - rerender the form
function lean() {
    let str = ` <form action="" method="get" class="tm-search-form tm-section-pad-2" id="leanSearchForm">
                                <div class="form-row tm-search-form-row">
                                    <div class="form-group tm-form-group tm-form-group-pad tm-form-group-1">
                                        <label for="inputCity">Choose Your Destination</label>
                                        <select name="destination" class="form-control tm-select" id="inputDestination"></select>
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-1">
                                        <div class="form-group tm-form-group tm-form-group-pad tm-form-group-2">
                                            <label for="inputCheckIn">Check In Date</label>
                                            <input name="check-in" type="text" class="form-control" id="inputCheckIn" placeholder="Check In" required onkeydown="event.preventDefault()">
                                        </div>
                                        <div class="form-group tm-form-group tm-form-group-pad tm-form-group-1">
                                            <label for="inputCheckOut">Check Out Date</label>
                                            <input name="check-out" type="text" class="form-control" id="inputCheckOut" placeholder="Check Out" required onkeydown="event.preventDefault()">
                                        </div>
                                    </div>
                                </div> <!-- form-row -->
                                <div class="form-row tm-search-form-row text-center row buttomRow">
                                    <div class="form-group tm-form-group">
                                        <button type="submit" class="btn btn-primary tm-btn tm-btn-search text-uppercase" id="btn_lean_Submit">Check Availability</button>
                                    </div>
                                    <div class="form-group tm-form-group tm-form-group-pad">
                                        <button type ="button" class="btn btn-primary tm-btn" onclick="advanced()">+</button>
                                    </div>
                                </div>
                            </form>`;
    $("#tm-section-search").html(str);
    setDestinations(); //in case there were more destinations other than Amsterdam

    //feature of selecting dates
    pickerCheckIn = datepicker('#inputCheckIn'); 
    pickerCheckOut = datepicker('#inputCheckOut');

    //lean search with only destination, check in date and check out date
    $("#leanSearchForm").submit(function () {
        let check_in = $("#inputCheckIn").val();
        let check_out = $("#inputCheckOut").val();

        check_in = SetDateFormat(check_in);
        check_out = SetDateFormat(check_out);
        let check_in_Date = new Date(check_in);
        let check_out_Date = new Date(check_out);
        let today = new Date();
        if (today.getTime() > check_in_Date.getTime()) {
            swal("Error picking dates", "Check in cannot be before today", "error");
            return false;
        }
        if (check_in_Date.getTime() > check_out_Date.getTime()) {
            swal("Error picking dates", "Check out cannot be before check in", "error");
            return false;
        }
        SearchLeanProperty();
        return false;
    });

    if (localStorage.getItem("property_fill") != undefined)
        fillSearchForm();
}

//set destinations in the form - using distinct location in the sql query
function setDestinations() {
    let destinations = JSON.parse(localStorage.getItem("destinations"));
    let select = document.getElementById("inputDestination");
    for (var i = 0; i < destinations.length; i++) {
        let option = document.createElement("option");
        option.text = destinations[i];
        select.add(option);
    }
}

//fill the form with the search parameters after submitting search
function fillSearchForm() {
    let search_property = JSON.parse(localStorage.getItem("property"));
    let property_fill = JSON.parse(localStorage.getItem("property_fill"));
    $("#inputCheckIn").val(property_fill.Check_in);
    $("#inputCheckOut").val(property_fill.Check_out);

    //if the search is advanced
    if (search_property.Bedrooms != undefined) {
        $("#inputRoom").val(property_fill.Bedrooms);
        $("#ReviewScore").val(property_fill.Review_scores_rating);
        $("#minPrice").val(property_fill.Min_price);
        $("#maxPrice").val(property_fill.Max_price);
        $("#DistanceFromCenter").val(property_fill.Center_distance);
        $("#DistanceFromCenterOut").val(property_fill.Center_distance + "  KM");
    }
}



