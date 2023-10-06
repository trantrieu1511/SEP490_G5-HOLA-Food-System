import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { AppBreadcrumbService } from '../../../../app-systems/app-breadcrumb/app.breadcrumb.service';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {

    cities: SelectItem[];

    products: any[];

    ordersChart: any;

    ordersOptions: any;

    selectedCity: any;

    timelineEvents: any[];

    overviewChartData1: any;

    overviewChartData2: any;

    overviewChartData3: any;

    overviewChartData4: any;

    overviewChartOptions1: any;

    overviewChartOptions2: any;

    overviewChartOptions3: any;

    overviewChartOptions4: any;

    chatMessages: any[];

    chatEmojis: any[];

    @ViewChild('chatcontainer') chatContainerViewChild: ElementRef;

    constructor(public layoutService: LayoutService, private breadcrumbService: AppBreadcrumbService) {
        this.breadcrumbService.setItems([
            { label: 'Dashboard', routerLink: ['/'] }
        ]);
    }

    ngOnInit() {
        this.products = [
            {
                "id": "1000",
                "code": "f230fh0g3",
                "name": "Bamboo Watch",
                "description": "Product Description",
                "image": "bamboo-watch.jpg",
                "price": 65,
                "category": "Accessories",
                "quantity": 24,
                "inventoryStatus": "INSTOCK",
                "rating": 5
            },
            {
                "id": "1001",
                "code": "nvklal433",
                "name": "Black Watch",
                "description": "Product Description",
                "image": "black-watch.jpg",
                "price": 72,
                "category": "Accessories",
                "quantity": 61,
                "inventoryStatus": "OUTOFSTOCK",
                "rating": 4
            },
            {
                "id": "1002",
                "code": "zz21cz3c1",
                "name": "Blue Band",
                "description": "Product Description",
                "image": "blue-band.jpg",
                "price": 79,
                "category": "Fitness",
                "quantity": 2,
                "inventoryStatus": "LOWSTOCK",
                "rating": 3
            },
            {
                "id": "1003",
                "code": "244wgerg2",
                "name": "Blue T-Shirt",
                "description": "Product Description",
                "image": "blue-t-shirt.jpg",
                "price": 29,
                "category": "Clothing",
                "quantity": 25,
                "inventoryStatus": "INSTOCK",
                "rating": 5
            },
            {
                "id": "1004",
                "code": "h456wer53",
                "name": "Bracelet",
                "description": "Product Description",
                "image": "bracelet.jpg",
                "price": 15,
                "category": "Accessories",
                "quantity": 73,
                "inventoryStatus": "INSTOCK",
                "rating": 4
            },
            {
                "id": "1005",
                "code": "av2231fwg",
                "name": "Brown Purse",
                "description": "Product Description",
                "image": "brown-purse.jpg",
                "price": 120,
                "category": "Accessories",
                "quantity": 0,
                "inventoryStatus": "OUTOFSTOCK",
                "rating": 4
            },
            {
                "id": "1006",
                "code": "bib36pfvm",
                "name": "Chakra Bracelet",
                "description": "Product Description",
                "image": "chakra-bracelet.jpg",
                "price": 32,
                "category": "Accessories",
                "quantity": 5,
                "inventoryStatus": "LOWSTOCK",
                "rating": 3
            },
            {
                "id": "1007",
                "code": "mbvjkgip5",
                "name": "Galaxy Earrings",
                "description": "Product Description",
                "image": "galaxy-earrings.jpg",
                "price": 34,
                "category": "Accessories",
                "quantity": 23,
                "inventoryStatus": "INSTOCK",
                "rating": 5
            },
            {
                "id": "1008",
                "code": "vbb124btr",
                "name": "Game Controller",
                "description": "Product Description",
                "image": "game-controller.jpg",
                "price": 99,
                "category": "Electronics",
                "quantity": 2,
                "inventoryStatus": "LOWSTOCK",
                "rating": 4
            },
            {
                "id": "1009",
                "code": "cm230f032",
                "name": "Gaming Set",
                "description": "Product Description",
                "image": "gaming-set.jpg",
                "price": 299,
                "category": "Electronics",
                "quantity": 63,
                "inventoryStatus": "INSTOCK",
                "rating": 3
            },
            {
                "id": "1010",
                "code": "plb34234v",
                "name": "Gold Phone Case",
                "description": "Product Description",
                "image": "gold-phone-case.jpg",
                "price": 24,
                "category": "Accessories",
                "quantity": 0,
                "inventoryStatus": "OUTOFSTOCK",
                "rating": 4
            },
            {
                "id": "1011",
                "code": "4920nnc2d",
                "name": "Green Earbuds",
                "description": "Product Description",
                "image": "green-earbuds.jpg",
                "price": 89,
                "category": "Electronics",
                "quantity": 23,
                "inventoryStatus": "INSTOCK",
                "rating": 4
            },
            {
                "id": "1012",
                "code": "250vm23cc",
                "name": "Green T-Shirt",
                "description": "Product Description",
                "image": "green-t-shirt.jpg",
                "price": 49,
                "category": "Clothing",
                "quantity": 74,
                "inventoryStatus": "INSTOCK",
                "rating": 5
            },
            {
                "id": "1013",
                "code": "fldsmn31b",
                "name": "Grey T-Shirt",
                "description": "Product Description",
                "image": "grey-t-shirt.jpg",
                "price": 48,
                "category": "Clothing",
                "quantity": 0,
                "inventoryStatus": "OUTOFSTOCK",
                "rating": 3
            },
            {
                "id": "1014",
                "code": "waas1x2as",
                "name": "Headphones",
                "description": "Product Description",
                "image": "headphones.jpg",
                "price": 175,
                "category": "Electronics",
                "quantity": 8,
                "inventoryStatus": "LOWSTOCK",
                "rating": 5
            },
            {
                "id": "1015",
                "code": "vb34btbg5",
                "name": "Light Green T-Shirt",
                "description": "Product Description",
                "image": "light-green-t-shirt.jpg",
                "price": 49,
                "category": "Clothing",
                "quantity": 34,
                "inventoryStatus": "INSTOCK",
                "rating": 4
            },
            {
                "id": "1016",
                "code": "k8l6j58jl",
                "name": "Lime Band",
                "description": "Product Description",
                "image": "lime-band.jpg",
                "price": 79,
                "category": "Fitness",
                "quantity": 12,
                "inventoryStatus": "INSTOCK",
                "rating": 3
            },
            {
                "id": "1017",
                "code": "v435nn85n",
                "name": "Mini Speakers",
                "description": "Product Description",
                "image": "mini-speakers.jpg",
                "price": 85,
                "category": "Clothing",
                "quantity": 42,
                "inventoryStatus": "INSTOCK",
                "rating": 4
            },
            {
                "id": "1018",
                "code": "09zx9c0zc",
                "name": "Painted Phone Case",
                "description": "Product Description",
                "image": "painted-phone-case.jpg",
                "price": 56,
                "category": "Accessories",
                "quantity": 41,
                "inventoryStatus": "INSTOCK",
                "rating": 5
            },];

        this.cities = [];
        this.cities.push({ label: 'Select City', value: null });
        this.cities.push({ label: 'New York', value: { id: 1, name: 'New York', code: 'NY' } });
        this.cities.push({ label: 'Rome', value: { id: 2, name: 'Rome', code: 'RM' } });
        this.cities.push({ label: 'London', value: { id: 3, name: 'London', code: 'LDN' } });
        this.cities.push({ label: 'Istanbul', value: { id: 4, name: 'Istanbul', code: 'IST' } });
        this.cities.push({ label: 'Paris', value: { id: 5, name: 'Paris', code: 'PRS' } });

        this.chatMessages = [
            { from: 'Ioni Bowcher', url: 'assets/demo/images/avatar/ionibowcher.png', messages: ['Hey M. hope you are well.', 'Our idea is accepted by the board. Now it’s time to execute it'] },
            { messages: ['We did it! 🤠'] },
            { from: 'Ioni Bowcher', url: 'assets/demo/images/avatar/ionibowcher.png', messages: ['That\'s really good!'] },
            { messages: ['But it’s important to ship MVP ASAP'] },
            { from: 'Ioni Bowcher', url: 'assets/demo/images/avatar/ionibowcher.png', messages: ['I’ll be looking at the process then, just to be sure 🤓'] },
            { messages: ['That’s awesome. Thanks!'] }
        ];

        this.chatEmojis = [
            '😀', '😃', '😄', '😁', '😆', '😅', '😂', '🤣', '😇', '😉', '😊', '🙂', '🙃', '😋', '😌', '😍', '🥰', '😘', '😗', '😙', '😚', '🤪', '😜', '😝', '😛',
            '🤑', '😎', '🤓', '🧐', '🤠', '🥳', '🤗', '🤡', '😏', '😶', '😐', '😑', '😒', '🙄', '🤨', '🤔', '🤫', '🤭', '🤥', '😳', '😞', '😟', '😠', '😡', '🤬', '😔',
            '😟', '😠', '😡', '🤬', '😔', '😕', '🙁', '😬', '🥺', '😣', '😖', '😫', '😩', '🥱', '😤', '😮', '😱', '😨', '😰', '😯', '😦', '😧', '😢', '😥', '😪', '🤤'
        ];

        this.ordersChart = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
            datasets: [{
                label: 'New Orders',
                data: [31, 83, 69, 29, 62, 25, 59, 26, 46],
                borderColor: [
                    '#4DD0E1',
                ],
                backgroundColor: [
                    'rgba(77, 208, 225, 0.8)',
                ],
                borderWidth: 2,
                fill: true,
                tension: .4
            }, {
                label: 'Completed Orders',
                data: [67, 98, 27, 88, 38, 3, 22, 60, 56],
                borderColor: [
                    '#3F51B5',
                ],
                backgroundColor: [
                    'rgba(63, 81, 181, 0.8)',
                ],
                borderWidth: 2,
                fill: true,
                tension: .4
            }]
        };

        this.ordersOptions = this.getOrdersOptions();

        this.overviewChartData1 = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
            datasets: [
                {
                    data: [50, 64, 32, 24, 18, 27, 20, 36, 30],
                    borderColor: [
                        '#4DD0E1',
                    ],
                    backgroundColor: [
                        'rgba(77, 208, 225, 0.8)',
                    ],
                    borderWidth: 2,
                    fill: true,
                    tension: .4
                }
            ]
        };

        this.overviewChartData2 = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
            datasets: [
                {
                    data: [11, 30, 52, 35, 39, 20, 14, 18, 29],
                    borderColor: [
                        '#4DD0E1',
                    ],
                    backgroundColor: [
                        'rgba(77, 208, 225, 0.8)',
                    ],
                    borderWidth: 2,
                    fill: true,
                    tension: .4
                }
            ]
        };

        this.overviewChartData3 = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
            datasets: [
                {
                    data: [20, 29, 39, 36, 45, 24, 28, 20, 15],
                    borderColor: [
                        '#4DD0E1',
                    ],
                    backgroundColor: [
                        'rgba(77, 208, 225, 0.8)',
                    ],
                    borderWidth: 2,
                    fill: true,
                    tension: .4
                }
            ]
        };

        this.overviewChartData4 = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
            datasets: [
                {
                    data: [30, 39, 50, 21, 33, 18, 10, 24, 20],
                    borderColor: [
                        '#4DD0E1',
                    ],
                    backgroundColor: [
                        'rgba(77, 208, 225, 0.8)',
                    ],
                    borderWidth: 2,
                    fill: true,
                    tension: .4
                }
            ]
        };

        this.overviewChartOptions1 = {
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    display: false
                },
                x: {
                    display: false
                }
            },
            tooltips: {
                enabled: false
            },
            elements: {
                point: {
                    radius: 0
                }
            },
        };

        this.overviewChartOptions2 = {
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    display: false
                },
                x: {
                    display: false
                }
            },
            tooltips: {
                enabled: false
            },
            elements: {
                point: {
                    radius: 0
                }
            },
        };

        this.overviewChartOptions3 = {
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    display: false
                },
                x: {
                    display: false
                }
            },
            tooltips: {
                enabled: false
            },
            elements: {
                point: {
                    radius: 0
                }
            },
        };

        this.overviewChartOptions4 = {
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    display: false
                },
                x: {
                    display: false
                }
            },
            tooltips: {
                enabled: false
            },
            elements: {
                point: {
                    radius: 0
                }
            },
        };


        this.timelineEvents = [
            { status: 'Ordered', date: '15/10/2020 10:30', icon: 'pi pi-shopping-cart', color: '#E91E63', description: 'Richard Jones (C8012) has ordered a blue t-shirt for $79.' },
            { status: 'Processing', date: '15/10/2020 14:00', icon: 'pi pi-cog', color: '#FB8C00', description: 'Order #99207 has processed succesfully.' },
            { status: 'Shipped', date: '15/10/2020 16:15', icon: 'pi pi-compass', color: '#673AB7', description: 'Order #99207 has shipped with shipping code 2222302090.' },
            { status: 'Delivered', date: '16/10/2020 10:00', icon: 'pi pi-check-square', color: '#0097A7', description: 'Richard Jones (C8012) has recieved his blue t-shirt.' }
        ];
    }

    onEmojiClick(chatInput, emoji) {
        if (chatInput) {
            chatInput.value += emoji;
            chatInput.focus();
        }
    }

    onChatKeydown(event) {
        if (event.key === 'Enter') {
            const message = event.currentTarget.value;
            const lastMessage = this.chatMessages[this.chatMessages.length - 1];

            if (lastMessage.from) {
                this.chatMessages.push({ messages: [message] });
            }
            else {
                lastMessage.messages.push(message);
            }

            if (message.match(/primeng|primereact|primefaces|primevue/i)) {
                this.chatMessages.push({ from: 'Ioni Bowcher', url: 'assets/demo/images/avatar/ionibowcher.png', messages: ['Always bet on Prime!'] });
            }

            event.currentTarget.value = '';

            const el = this.chatContainerViewChild.nativeElement;
            setTimeout(() => {
                el.scroll({
                    top: el.scrollHeight,
                    behavior: 'smooth'
                });
            }, 1);
        }
    }


    getOrdersOptions() {
        const textColor = getComputedStyle(document.body).getPropertyValue('--text-color') || 'rgba(0, 0, 0, 0.87)';
        const gridLinesColor = getComputedStyle(document.body).getPropertyValue('--divider-color') || 'rgba(160, 167, 181, .3)';
        const fontFamily = getComputedStyle(document.body).getPropertyValue('--font-family');
        return {
            plugins: {
                legend: {
                    display: true,
                    labels: {
                        fontFamily,
                        fontColor: textColor,
                    }
                }
            },
            responsive: true,
            scales: {
                y: {
                    ticks: {
                        fontFamily,
                        color: textColor
                    },
                    grid: {
                        color: gridLinesColor
                    }
                },
                x: {
                    ticks: {
                        fontFamily,
                        color: textColor
                    },
                    grid: {
                        color: gridLinesColor
                    }
                }
            }
        };
    }
}
