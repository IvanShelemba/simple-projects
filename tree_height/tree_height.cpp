#include <iostream>
#include <vector>

class Node;

class Node{
    public:
    int key;
    Node* parent;
    std::vector<Node*> children;

    Node() 
    {
        this->parent = NULL;
    }

    void setParent(Node* theParent) {
        parent = theParent;
        parent->children.push_back(this);
    }
};

int getDepth(Node* node, int d);

int main()
{
    std::ios_base::sync_with_stdio(0);
    int n;
    std::cin >> n;

    std::vector<Node> nodes;

    nodes.resize(n);

    int parent_index;

    Node* root = new Node();

    for (int child_index = 0; child_index < n; child_index++) 
    {
        std::cin >> parent_index;

        if (parent_index >= 0) nodes[child_index].setParent(&nodes[parent_index]);
        else root = &nodes[child_index];

        nodes[child_index].key = child_index;
    }

    std::cout << getDepth(root, 1);

    return 0;
}

int getDepth(Node* node, int d)
{
    int t;
    int ret = d;

    for (int i = 0; i < node->children.size(); i++)
    {
        t = getDepth(node->children[i], d + 1);
        if (ret < t) ret = t;
    }

    return ret;
}
