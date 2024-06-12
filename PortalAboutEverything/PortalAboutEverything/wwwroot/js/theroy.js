// Outdated
var name = "Ivan";
let nameCanBeChange = "Olga";
nameCanBeChange = "Lear";
const nameNeverChange = "Jim";
nameNeverChange = "Bob"; // Error

let name2 = 'test';
let name3 = "I'm";

let number = 1;
let chance = .05;

let obj = 'text';
obj = 12;
obj = false;

let user1 = {
	name: 'Ivan',
	age: 18,
	isMan: true
};

let user2 = {
	name: 'Olga',
	age: 20,
	isMan: false
};

user1.city = 'Minsk';

function SayHi1() {
	console.log('hi 1');
}

let SayHi2 = function () {
	console.log('hi 2');
}

SayHi1();
SayHi2();

function SayHi() {
	console.log(`Hi. My name is ${this.name}. I'm ${this.age} old`);
}

user2.speak = SayHi;
user2.speak();


const user3 = {
	name: 'Tim',
	age: 50,
	speak: function () { console.log(this.name) }
};

let user4; // undefined => null


if (user1) {
	console.log(user1.name);
}

if (user4) {
	console.log(user4.name);
}

const ageFromUser = $('input[name=age]').val() - 0;
const newAge = ageFromUser + 18;


const ages = [20, {}, true, 'qwe']

ages.forEach((ele, i) => {
	console.log(`index: ${i}) ${ele}`);
})

for (age in ages) {

}





