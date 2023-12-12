    public abstract class ListBoxDropHandler : DefaultDropHandler
    {
        internal bool ValidateLibrary<T>(ListBox listBox, DragEventArgs e, object sourceContext, object targetContext, bool bExecute) where T : ViewModelBase
        {
            if (!(sourceContext is T sourceItem) 
                || !(targetContext is Library<T> library) 
                || !(listBox.GetVisualAt(e.GetPosition(listBox)) is IControl targetControl)
                || !(listBox.GetVisualRoot() is IControl rootControl)
                || !(rootControl.DataContext is ProjectEditor editor)
                || !(targetControl.DataContext is T targetItem))
            {
                return false;
            }

            int sourceIndex = library.Items.IndexOf(sourceItem);
            int targetIndex = library.Items.IndexOf(targetItem);

            if (sourceIndex < 0 || targetIndex < 0)
            {
                return false;
            }

            if (e.DragEffects == DragDropEffects.Copy)
            {
                if (bExecute)
                {
                    var clone = (T)sourceItem.Copy(null);
                    clone.Name += "-copy";
                    editor.InsertItem(library, clone, targetIndex + 1);
                }
                return true;
            }
            else if (e.DragEffects == DragDropEffects.Move)
            {
                if (bExecute)
                {
                    editor.MoveItem(library, sourceIndex, targetIndex);
                }
                return true;
            }
            else if (e.DragEffects == DragDropEffects.Link)
            {
                if (bExecute)
                {
                    editor.SwapItem(library, sourceIndex, targetIndex);
                }
                return true;
            }

            return false;
        }
    }