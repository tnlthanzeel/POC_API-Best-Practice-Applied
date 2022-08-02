using AutoMapper;
using VPMS.Application.Contracts.ToDoItem;
using VPMS.Application.PersistanceInterfaces;
using VPMS.Application.TodoItem.Dtos;
using VPMS.Application.TodoItem.Specs;
using VPMS.Application.TodoItem.Validators;
using VPMS.SharedKernel.Exceptions;
using VPMS.SharedKernel.Models;

namespace VPMS.Application.TodoItem;

public sealed class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly IMapper _mapper;

    public TodoItemService(ITodoItemRepository todoItemRepository, IMapper mapper)
    {
        _todoItemRepository = todoItemRepository;
        _mapper = mapper;
    }
    public async Task<ResponseResult<TodoItemDto>> Add(CreateTodoItemDto model, CancellationToken token)
    {
        var validationResult = await Validator<CreateTodoItemDtoValidator>.ValidateAsync(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult<TodoItemDto>(validationResult.Errors);

        var itemtoCreate = _mapper.Map<Domain.Entities.TodoItemEntities.TodoItem>(model);

        var item = _todoItemRepository.Add(itemtoCreate);

        await _todoItemRepository.SaveChangesAsync(token);

        var newItem = _mapper.Map<TodoItemDto>(item);

        var responseResult = new ResponseResult<TodoItemDto>(newItem);

        return responseResult;
    }

    public async Task<ResponseResult<TodoItemDto>> GetById(Guid id, CancellationToken token)
    {
        var todoItem = await _todoItemRepository.GetById(id, token);

        if (todoItem is null) return new ResponseResult<TodoItemDto>(new NotFoundException(nameof(id), nameof(TodoItem), id));

        var todoItemDto = _mapper.Map<TodoItemDto>(todoItem);

        return new ResponseResult<TodoItemDto>(todoItemDto);
    }

    public async Task<ResponseResult<IReadOnlyList<TodoItemDto>>> GetList(Paginator paginator, ToDoItemFilter filter, CancellationToken token)
    {
        var (list, totalRecords) = await _todoItemRepository.GetListBySpec(paginator, new ToDoItemFilterSpec(filter), token);

        var todoItemDtoList = _mapper.Map<IReadOnlyList<TodoItemDto>>(list);

        return new ResponseResult<IReadOnlyList<TodoItemDto>>(todoItemDtoList, totalRecords);
    }

    public async Task<ResponseResult> Update(Guid id, UpdateTodoItemDto model, CancellationToken token)
    {
        var validationResult = await Validator<UpdateTodoItemDtoValidator>.ValidateAsync(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult(validationResult.Errors);

        var todoItem = await _todoItemRepository.GetById(id, asTracking: true, token: token);

        if (todoItem is null) return new ResponseResult(new NotFoundException(nameof(id), nameof(TodoItem), id));

        _mapper.Map(model, todoItem);

        await _todoItemRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }

    public async Task<ResponseResult> Delete(Guid id, CancellationToken token)
    {
        var todoItem = await _todoItemRepository.GetById(id, asTracking: true, token: token);

        if (todoItem is null) return new ResponseResult(new NotFoundException(nameof(id), nameof(TodoItem), id));

        todoItem.Delete();

        await _todoItemRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }


}
