﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab06
{
    
    // Represents a node in a doubly linked list.
    
    public class DoublyLinkedListNode<T>
    {
        public T Value { get; set; } // The data stored in the node
        public DoublyLinkedListNode<T> Next { get; set; } // Pointer to the next node
        public DoublyLinkedListNode<T> Previous { get; set; } // Pointer to the previous node

        
        // Initializes a new node with a given value.
        
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }
    }

    
    // Defines the interface for a double-ended collection (doubly linked list).
    
    public interface IDoubleEndedCollection<T>
    {
        T First { get; } // Gets the first element
        T Last { get; } // Gets the last element
        int Length { get; } // Gets the number of elements in the list

        void AddLast(T value); // Adds an element to the end
        void AddFirst(T value); // Adds an element to the front
        void RemoveFirst(); // Removes the first element
        void RemoveLast(); // Removes the last element
        void InsertAfter(DoublyLinkedListNode<T> node, T value); // Inserts an element after a given node
        void RemoveByValue(T value); // Removes a node by value
        void ReverseList(); // Reverses the linked list
    }

    
    // Implements a doubly linked list.
    
    public class DoublyLinkedList<T> : IDoubleEndedCollection<T>, IEnumerable<T>
    {
        private DoublyLinkedListNode<T> _head; // First node in the list
        private DoublyLinkedListNode<T> _tail; // Last node in the list
        private int _count; // Number of nodes in the list

        public int Length => _count; // Returns the total number of elements in the list
        public T First => _head != null ? _head.Value : throw new InvalidOperationException("The list is empty.");
        public T Last => _tail != null ? _tail.Value : throw new InvalidOperationException("The list is empty.");

        
        /// Adds a new node at the beginning of the list.
        
        public void AddFirst(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(value);
            if (_head == null)
            {
                _head = _tail = newNode; // If list is empty, head and tail are the same
            }
            else
            {
                newNode.Next = _head;
                _head.Previous = newNode;
                _head = newNode; // Update head
            }
            _count++;
        }

        
        /// Adds a new node at the end of the list.
        
        public void AddLast(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(value);
            if (_tail == null)
            {
                _head = _tail = newNode; // If list is empty, head and tail are the same
            }
            else
            {
                newNode.Previous = _tail;
                _tail.Next = newNode;
                _tail = newNode; // Update tail
            }
            _count++;
        }

        
        // Removes the first node from the list.
        
        public void RemoveFirst()
        {
            if (_head == null) return; // Do nothing if list is empty

            if (_head == _tail)
            {
                _head = _tail = null; // If only one element, reset list
            }
            else
            {
                _head = _head.Next;
                _head.Previous = null;
            }
            _count--;
        }

        
        // Removes the last node from the list.
        
        public void RemoveLast()
        {
            if (_tail == null) return; // Do nothing if list is empty

            if (_head == _tail)
            {
                _head = _tail = null; // If only one element, reset list
            }
            else
            {
                _tail = _tail.Previous;
                _tail.Next = null;
            }
            _count--;
        }

        
        // Inserts a new node after a given node in the list.
        
        public void InsertAfter(DoublyLinkedListNode<T> node, T value)
        {
            if (node == null) return;

            var newNode = new DoublyLinkedListNode<T>(value);
            newNode.Next = node.Next;
            newNode.Previous = node;

            if (node.Next != null)
            {
                node.Next.Previous = newNode;
            }
            node.Next = newNode;

            if (node == _tail)
            {
                _tail = newNode; // If inserting after the last node, update tail
            }
            _count++;
        }

        
        // removes the first node that matches the given value.
        
        public void RemoveByValue(T value)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    if (current == _head)
                    {
                        RemoveFirst();
                    }
                    else if (current == _tail)
                    {
                        RemoveLast();
                    }
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                        _count--;
                    }
                    return; // Exit after removing the first match
                }
                current = current.Next;
            }
        }

        
        // Reverses the order of nodes in the list.
        
        public void ReverseList()
        {
            var current = _head;
            DoublyLinkedListNode<T> temp = null;

            while (current != null)
            {
                // Swap previous and next pointers
                temp = current.Previous;
                current.Previous = current.Next;
                current.Next = temp;
                current = current.Previous; // Move to next node (which is now previous)
            }

            // Swap head and tail
            if (temp != null)
            {
                _tail = _head;
                _head = temp.Previous;
            }
        }

        
        // returns an enumerator that iterates through the list.
        
        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
