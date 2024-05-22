#include <stdio.h>
#include <stdlib.h>
#include <string.h>


typedef struct {
    int id;
    char name[50];
    float price;
    int quantity;
} Item;


Item createItem(int id, const char* name, float price, int quantity);
void printItem(const Item* item);


typedef struct {
    Item* items;
    int size;
    int capacity;
} Inventory;


Inventory createInventory(int capacity);
void addItem(Inventory* inventory, Item item);
void displayItems(const Inventory* inventory);
Item* findItemById(const Inventory* inventory, int id);
void updateItem(Item* item, const char* name, float price, int quantity);
void deleteItem(Inventory* inventory, int id);
void freeInventory(Inventory* inventory);

int main() {
    Inventory inventory = createInventory(10);
    int choice, id, quantity;
    char name[50];
    float price;

    while (1) {
        printf("\nInventory Management System\n");
        printf("1. Add Item\n");
        printf("2. Display Items\n");
        printf("3. Find Item by ID\n");
        printf("4. Update Item\n");
        printf("5. Delete Item\n");
        printf("6. Exit\n");
        printf("Enter your choice: ");
        scanf("%d", &choice);

        switch (choice) {
            case 1:
                printf("Enter ID: ");
                scanf("%d", &id);
                printf("Enter Name: ");
                scanf("%s", name);
                printf("Enter Price: ");
                scanf("%f", &price);
                printf("Enter Quantity: ");
                scanf("%d", &quantity);
                addItem(&inventory, createItem(id, name, price, quantity));
                break;
            case 2:
                displayItems(&inventory);
                break;
            case 3:
                printf("Enter ID: ");
                scanf("%d", &id);
                Item* item = findItemById(&inventory, id);
                if (item) {
                    printItem(item);
                } else {
                    printf("Item not found.\n");
                }
                break;
            case 4:
                printf("Enter ID of item to update: ");
                scanf("%d", &id);
                item = findItemById(&inventory, id);
                if (item) {
                    printf("Enter new Name: ");
                    scanf("%s", name);
                    printf("Enter new Price: ");
                    scanf("%f", &price);
                    printf("Enter new Quantity: ");
                    scanf("%d", &quantity);
                    updateItem(item, name, price, quantity);
                } else {
                    printf("Item not found.\n");
                }
                break;
            case 5:
                printf("Enter ID of item to delete: ");
                scanf("%d", &id);
                deleteItem(&inventory, id);
                break;
            case 6:
                freeInventory(&inventory);
                printf("Exiting...\n");
                return 0;
            default:
                printf("Invalid choice. Try again.\n");
        }
    }
}


Item createItem(int id, const char* name, float price, int quantity) {
    Item item;
    item.id = id;
    strncpy(item.name, name, sizeof(item.name));
    item.price = price;
    item.quantity = quantity;
    return item;
}

void printItem(const Item* item) {
    printf("ID: %d, Name: %s, Price: %.2f, Quantity: %d\n", item->id, item->name, item->price, item->quantity);
}


Inventory createInventory(int capacity) {
    Inventory inventory;
    inventory.items = (Item*)malloc(sizeof(Item) * capacity);
    inventory.size = 0;
    inventory.capacity = capacity;
    return inventory;
}

void addItem(Inventory* inventory, Item item) {
    if (inventory->size >= inventory->capacity) {
        inventory->capacity *= 2;
        inventory->items = (Item*)realloc(inventory->items, sizeof(Item) * inventory->capacity);
    }
    inventory->items[inventory->size++] = item;
}

void displayItems(const Inventory* inventory) {
    for (int i = 0; i < inventory->size; ++i) {
        printItem(&inventory->items[i]);
    }
}

Item* findItemById(const Inventory* inventory, int id) {
    for (int i = 0; i < inventory->size; ++i) {
        if (inventory->items[i].id == id) {
            return &inventory->items[i];
        }
    }
    return NULL;
}

void updateItem(Item* item, const char* name, float price, int quantity) {
    strncpy(item->name, name, sizeof(item->name));
    item->price = price;
    item->quantity = quantity;
}

void deleteItem(Inventory* inventory, int id) {
    for (int i = 0; i < inventory->size; ++i) {
        if (inventory->items[i].id == id) {
            for (int j = i; j < inventory->size - 1; ++j) {
                inventory->items[j] = inventory->items[j + 1];
            }
            --inventory->size;
            return;
        }
    }
    printf("Item not found.\n");
}

void freeInventory(Inventory* inventory) {
    free(inventory->items);
    inventory->items = NULL;
    inventory->size = 0;
    inventory->capacity = 0;
}#include <stdio.h>
#include <stdlib.h>
#include <string.h>


typedef struct {
    int id;
    char name[50];
    float price;
    int quantity;
} Item;


Item createItem(int id, const char* name, float price, int quantity);
void printItem(const Item* item);


typedef struct {
    Item* items;
    int size;
    int capacity;
} Inventory;


Inventory createInventory(int capacity);
void addItem(Inventory* inventory, Item item);
void displayItems(const Inventory* inventory);
Item* findItemById(const Inventory* inventory, int id);
void updateItem(Item* item, const char* name, float price, int quantity);
void deleteItem(Inventory* inventory, int id);
void freeInventory(Inventory* inventory);

int main() {
    Inventory inventory = createInventory(10);
    int choice, id, quantity;
    char name[50];
    float price;

    while (1) {
        printf("\nInventory Management System\n");
        printf("1. Add Item\n");
        printf("2. Display Items\n");
        printf("3. Find Item by ID\n");
        printf("4. Update Item\n");
        printf("5. Delete Item\n");
        printf("6. Exit\n");
        printf("Enter your choice: ");
        scanf("%d", &choice);

        switch (choice) {
            case 1:
                printf("Enter ID: ");
                scanf("%d", &id);
                printf("Enter Name: ");
                scanf("%s", name);
                printf("Enter Price: ");
                scanf("%f", &price);
                printf("Enter Quantity: ");
                scanf("%d", &quantity);
                addItem(&inventory, createItem(id, name, price, quantity));
                break;
            case 2:
                displayItems(&inventory);
                break;
            case 3:
                printf("Enter ID: ");
                scanf("%d", &id);
                Item* item = findItemById(&inventory, id);
                if (item) {
                    printItem(item);
                } else {
                    printf("Item not found.\n");
                }
                break;
            case 4:
                printf("Enter ID of item to update: ");
                scanf("%d", &id);
                item = findItemById(&inventory, id);
                if (item) {
                    printf("Enter new Name: ");
                    scanf("%s", name);
                    printf("Enter new Price: ");
                    scanf("%f", &price);
                    printf("Enter new Quantity: ");
                    scanf("%d", &quantity);
                    updateItem(item, name, price, quantity);
                } else {
                    printf("Item not found.\n");
                }
                break;
            case 5:
                printf("Enter ID of item to delete: ");
                scanf("%d", &id);
                deleteItem(&inventory, id);
                break;
            case 6:
                freeInventory(&inventory);
                printf("Exiting...\n");
                return 0;
            default:
                printf("Invalid choice. Try again.\n");
        }
    }
}


Item createItem(int id, const char* name, float price, int quantity) {
    Item item;
    item.id = id;
    strncpy(item.name, name, sizeof(item.name));
    item.price = price;
    item.quantity = quantity;
    return item;
}

void printItem(const Item* item) {
    printf("ID: %d, Name: %s, Price: %.2f, Quantity: %d\n", item->id, item->name, item->price, item->quantity);
}


Inventory createInventory(int capacity) {
    Inventory inventory;
    inventory.items = (Item*)malloc(sizeof(Item) * capacity);
    inventory.size = 0;
    inventory.capacity = capacity;
    return inventory;
}

void addItem(Inventory* inventory, Item item) {
    if (inventory->size >= inventory->capacity) {
        inventory->capacity *= 2;
        inventory->items = (Item*)realloc(inventory->items, sizeof(Item) * inventory->capacity);
    }
    inventory->items[inventory->size++] = item;
}

void displayItems(const Inventory* inventory) {
    for (int i = 0; i < inventory->size; ++i) {
        printItem(&inventory->items[i]);
    }
}

Item* findItemById(const Inventory* inventory, int id) {
    for (int i = 0; i < inventory->size; ++i) {
        if (inventory->items[i].id == id) {
            return &inventory->items[i];
        }
    }
    return NULL;
}

void updateItem(Item* item, const char* name, float price, int quantity) {
    strncpy(item->name, name, sizeof(item->name));
    item->price = price;
    item->quantity = quantity;
}

void deleteItem(Inventory* inventory, int id) {
    for (int i = 0; i < inventory->size; ++i) {
        if (inventory->items[i].id == id) {
            for (int j = i; j < inventory->size - 1; ++j) {
                inventory->items[j] = inventory->items[j + 1];
            }
            --inventory->size;
            return;
        }
    }
    printf("Item not found.\n");
}

void freeInventory(Inventory* inventory) {
    free(inventory->items);
    inventory->items = NULL;
    inventory->size = 0;
    inventory->capacity = 0;
}