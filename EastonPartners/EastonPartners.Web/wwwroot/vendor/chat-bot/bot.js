const chat = document.querySelector('#chat'); //Chat area where messages are displayed
const textBox = document.querySelector('#text-box'); //Text box where user enters questions

//This function is called everytime the user click the "Go" button
function go() {
    var message = textBox.value; //Stores the user's question

    //Displays question on the screen
    const myText = document.createElement("p");
    myText.innerText = "Me: " + message;
    myText.style = "color: cyan;";

    chat.appendChild(myText);

    var isFound = false;

    //Answers how to log in
    if (message.toLowerCase().includes("login") || message.toLowerCase().includes("log in") || (message.toLowerCase().includes("access") && message.toLowerCase().includes("account"))) {
        const assisText = response(isFound);

        assisText.innerText += "To login to your RoverPartners account, you can: \n 1. Go to the Homepage \n 2. Click on Login \n 3. Enter your email and password then hit enter. \n";
        isFound = true;
        chat.appendChild(assisText);
    }

    //Tells user that they can't create an account for themselves
    if (((message.toLowerCase().includes("create") || message.toLowerCase().includes("make")) && message.toLowerCase().includes("account")) || message.toLowerCase().includes("register")) {
        const assisText = response(isFound);

        assisText.innerText += "You can't create an account or register on this app. You must have an account created by the school district in order to use RoverPartners. If you already have an account, login with your email and password. \n";
        isFound = true;
        chat.appendChild(assisText);
    }

    //Answers how to view the partner information
    if ((message.toLowerCase().includes("view") || message.toLowerCase().includes("manage") || message.toLowerCase().includes("edit")) && message.toLowerCase().includes("partner")) {
        const assisText = response(isFound);

        assisText.innerText += "If you're logged in, go to the homepage and click on the button that says \"Dashboard.\" In your dashboard you should see an option that says \"Partners,\" and if you click on it you should be able to view and manage the information about all the partners. \nNOTE: If you're not an admin you can only view the information but not modify it. \n";
        isFound = true;
        chat.appendChild(assisText);
    }

    //Answers how to manage users and roles
    if ((message.toLowerCase().includes("manage") || message.toLowerCase().includes("edit")) && (message.toLowerCase().includes("user") || message.toLowerCase().includes("role"))) {
        const assisText = response(isFound);

        assisText.innerText += "If you're an admin, and you'relogged in, go to the homepage and click on the button that says \"Dashboard.\" In your dashboard you should see two options, \"Users,\" and \"Roles.\" Click on one of them and you should be able to view, create, edit, and delete users or roles from the app. \nNOTE: If you're not an admin you don't have access to manage users or roles. \n";
        isFound = true;
        chat.appendChild(assisText);
    }

    //If no keywords are found in questions, ask user to rephrase question
    if (!isFound) {
        const assisText = response(isFound);
        assisText.innerText += "Sorry I didn't understand. Can you rephrase your question? \n";
        chat.appendChild(assisText);
    }
}

//Creates and returns a paragraph HTML element that represents the assistant's response
function response(isFound) {
    const assisText = document.createElement("p");
    if (isFound) assisText.innerText = "";
    else assisText.innerText = "Assistant: ";
    assisText.style = "color: red;";
    return assisText;
}