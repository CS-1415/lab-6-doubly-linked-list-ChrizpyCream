﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab06
{
    public class DoublyLinkedListNode<T>
    {
        public T Value { get; set; }
        public DoublyLinkedListNode<T> Next { get; set; }
        public DoublyLinkedListNode<T> Previous { get; set; }

        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }
    }

    public interface IDoubleEndedCollection<T>
    {
        T First { get; }
        T Last { get; }
        int Length { get; }
        void AddLast(T value);
        void AddFirst(T value);
        void RemoveFirst();
        void RemoveLast();
        void InsertAfter(DoublyLinkedListNode<T> node, T value);
        void RemoveByValue(T value);
        void ReverseList();
    }

    public class DoublyLinkedList<T> : IDoubleEndedCollection<T>, IEnumerable<T>
    {
        private DoublyLinkedListNode<T> _head;
        private DoublyLinkedListNode<T> _tail;
        private int _count;

        public int Length => _count;
        public T First => _head != null ? _head.Value : throw new InvalidOperationException("The list is empty.");
        public T Last => _tail != null ? _tail.Value : throw new InvalidOperationException("The list is empty.");

        public void AddFirst(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(value);
            if (_head == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head.Previous = newNode;
                _head = newNode;
            }
            _count++;
        }

        public void AddLast(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(value);
            if (_tail == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                newNode.Previous = _tail;
                _tail.Next = newNode;
                _tail = newNode;
            }
            _count++;
        }

        public void RemoveFirst()
        {
            if (_head == null) return;
            if (_head == _tail)
            {
                _head = _tail = null;
            }
            else
            {
                _head = _head.Next;
                _head.Previous = null;
            }
            _count--;
        }

        public void RemoveLast()
        {
            if (_tail == null) return;
            if (_head == _tail)
            {
                _head = _tail = null;
            }
            else
            {
                _tail = _tail.Previous;
                _tail.Next = null;
            }
            _count--;
        }

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
                _tail = newNode;
            }
            _count++;
        }

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
                    return;
                }
                current = current.Next;
            }
        }

        public void ReverseList()
        {
            var current = _head;
            DoublyLinkedListNode<T> temp = null;

            while (current != null)
            {
                temp = current.Previous;
                current.Previous = current.Next;
                current.Next = temp;
                current = current.Previous;
            }

            if (temp != null)
            {
                _tail = _head;
                _head = temp.Previous;
            }
        }

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
